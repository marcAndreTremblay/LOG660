CREATE OR REPLACE TRIGGER VerifierDateExpirationCarte
BEFORE INSERT ON CarteCredit
FOR EACH ROW
BEGIN
	IF :NEW.EXP_YEAR < EXTRACT(YEAR FROM SYSDATE) OR :NEW.EXP_YEAR = EXTRACT(YEAR FROM SYSDATE) AND :NEW.EXP_MONTH < EXTRACT(MONTH FROM SYSDATE) THEN
		RAISE_APPLICATION_ERROR('-20000', 'Carte de crédit expiré');
	END IF;
END;

/

CREATE OR REPLACE TRIGGER VerifierAgeClient
BEFORE INSERT ON Client
FOR EACH ROW
DECLARE
	DateNaissanceClient date;
BEGIN
	SELECT DateNaissance INTO DateNaissanceClient FROM Personne WHERE PersonneID = :NEW.PersonneID;
	IF  DateNaissanceClient > ADD_MONTHS(SYSDATE,-216) THEN /* Should remove days not month could have some probleme when the birthday very soon*/
		DELETE FROM Personne WHERE PersonneID = :NEW.PersonneID;
		RAISE_APPLICATION_ERROR('-20000', 'Le client doit avoir au moins 18 ans');
	END IF;
END;

/

CREATE OR REPLACE TRIGGER VerifierSiLocationDisponible
BEFORE INSERT ON Location_Client
FOR EACH ROW
DECLARE
	EstLoue INTEGER;
BEGIN
	SELECT COUNT(*) INTO EstLoue FROM Location_Client WHERE CodeCopieID = :NEW.CodeCopieID AND dateRetour IS NULL;
	IF EstLoue <> 0 THEN
		RAISE_APPLICATION_ERROR('-20000', 'La copie doit être disponible pour pouvoir la louer');
	END IF;
END;

/

CREATE OR REPLACE TRIGGER VerifierSiClientLouePlusQueMax
BEFORE INSERT ON Location_Client
FOR EACH ROW
DECLARE
	NBFilm INTEGER;
	NBFilmMax INTEGER;
BEGIN
	SELECT COUNT(*) INTO NBFilm FROM Location_Client WHERE CLIENTID = :NEW.CLIENTID AND dateRetour IS NULL;
	SELECT LocationMax INTO NBFilmMax FROM Client INNER JOIN forfait ON Client.FORFAITID = Forfait.FORFAITID WHERE Client.CLIENTID = :NEW.CLIENTID;
	IF NBFilm >= NBFilmMax THEN
		RAISE_APPLICATION_ERROR('-20000', 'Le client ne peut pas avoir plus de location que son forfait lui permet');
	END IF;
END;

/

create or replace PROCEDURE pCreerClient
    (prenom_in IN VARCHAR2, nomFamille_in IN VARCHAR2, dateNaissance_in IN DATE, numeroTel_in IN VARCHAR2, courriel_in IN VARCHAR2, password_in IN VARCHAR2, 
    noCivique_in IN VARCHAR2, rue_in IN VARCHAR2, ville_in IN VARCHAR2, province_in IN VARCHAR2, codePostal_in IN VARCHAR2, typeCarte_in IN VARCHAR2, numero_in IN NUMBER, 
    exp_month_in IN NUMBER, exp_year_in IN NUMBER, cvv_in IN NUMBER, forfaitID_in IN NUMBER)
IS
    adresseID NUMBER;
    personneID NUMBER;
    carteCreditID NUMBER;
BEGIN
    INSERT INTO Adresse (noCivique, rue, ville, province, codePostal)
    VALUES (noCivique_in, rue_in, ville_in, province_in, codePostal_in)
    RETURNING adresseID INTO adresseID;
    
    INSERT INTO CarteCredit (typeCarte, numero, exp_month, exp_year, cvv)
    VALUES (typeCarte_in, numero_in, exp_month_in, exp_year_in, cvv_in)
    RETURNING carteCreditID INTO carteCreditID;
    
    INSERT INTO Personne (prenom, nomFamille, dateNaissance)
    VALUES (prenom_in, nomFamille_in, dateNaissance_in)
    RETURNING personneID INTO personneID;

    INSERT INTO Client (adresseID, carteCreditID, personneID, forfaitID, courriel, numeroTel, password)
    VALUES (adresseID, carteCreditID, personneID, forfaitID_in, courriel_in, numeroTel_in, password_in);

EXCEPTION 
WHEN OTHERS THEN raise_application_error(-20001,'Erreur dans la procédure pCreerClient - '||SQLCODE||' -ERROR- '||SQLERRM);
END pCreerClient;

/


create or replace PROCEDURE pLouerFilm
    (filmID_in IN NUMBER, clientID_in IN NUMBER)
IS 
    copieALouer NUMBER;
BEGIN
    BEGIN
        /* Check for copies that have never been rented */
        SELECT inv.codeCopieID INTO copieALouer
        FROM Inventaire inv
        LEFT JOIN Location_Client lc ON lc.codeCopieID = inv.codeCopieID
        WHERE filmID = filmID_in
            AND lc.LOCATIONID IS NULL
            AND ROWNUM = 1;
            
        INSERT INTO Location_Client (codeCopieID, clientID, dateLocation)
        VALUES (copieALouer, clientID_in, SYSDATE); 
        
    EXCEPTION WHEN NO_DATA_FOUND THEN
        SELECT codeCopieID INTO copieALouer /* Copy for this movie that has been returned and not yet rented*/
        FROM Inventaire
        WHERE filmID = filmID_in 
            AND codeCopieID NOT IN 
            (
                SELECT codeCopieID   /* Rented copies */
                FROM Location_Client
                WHERE 
                    codeCopieID = ANY ( 
                        SELECT codeCopieID
                        FROM Inventaire
                        WHERE filmID = filmID_in
                    )
                AND dateRetour IS NULL
             )
            AND ROWNUM = 1;
        
        INSERT INTO Location_Client (codeCopieID, clientID, dateLocation)
        VALUES (copieALouer, clientID_in, SYSDATE); 
    END;
EXCEPTION WHEN NO_DATA_FOUND THEN
    raise_application_error(-20001,'Plus de copies disponibles pour le film ' || filmID_in);
END pLouerFilm;

/