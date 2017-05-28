CREATE TABLE  PERSONNE(
PersonneID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
prenom VARCHAR(100),
nomFamille VARCHAR(100),
dateNaissance DATE,
 biographie VARCHAR(2000), 
 lieuNaissance VARCHAR(100),
PRIMARY KEY(PersonneID));

CREATE TABLE Realisateur(
 fk_FilmID INTEGER,
 fk_PersonneID INTEGER);
 
CREATE TABLE Scenariste(
 fk_FilmID INTEGER,
 nom VARCHAR(50));
 


CREATE TABLE Client
(ClientID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
PersonneID INTEGER, 
ForfaitID INTEGER, 
AdresseID INTEGER,
CarteCreditID INTEGER, 
numeroTel VARCHAR(20) UNIQUE, 
courriel VARCHAR(50), 
password VARCHAR(100),  
PRIMARY KEY(ClientID),
CHECK (REGEXP_LIKE(password, '^[a-zA-Z0-9]{5,}$')));

CREATE TABLE Employe
(EmployeID iNTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
PersonneID INTEGER, 
AdresseID INTEGER, 
numeroTel VARCHAR(20), 
courriel VARCHAR(50) UNIQUE, 
password VARCHAR(100), 
matricule VARCHAR(7),  
PRIMARY KEY(EmployeID),
CHECK (REGEXP_LIKE(password, '^[a-zA-Z0-9]{5,}$')));

CREATE TABLE CarteCredit
(CarteCreditID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
typeCarte VARCHAR(10), 
numero INTEGER, 
exp_month INTEGER, 
exp_year INTEGER, 
cvv INTEGER,  
PRIMARY KEY(CarteCreditID));

CREATE TABLE Forfait
(ForfaitID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
coutParMois INTEGER, 
typeForfait VARCHAR(15), 
locationMax INTEGER, 
dureeMaxJour INTEGER,  
PRIMARY KEY(ForfaitID));

CREATE TABLE Film_Acteur(
fk_personneID INTEGER, 
fk_filmID INTEGER, 
personnage VARCHAR(50),
PRIMARY KEY(fk_personneID, fk_filmID));

CREATE TABLE Inventaire
(CodeCopieID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
FilmID INTEGER,  
PRIMARY KEY(CodeCopieID));

CREATE TABLE Adresse
(AdresseID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
noCivique VARCHAR(10), 
rue VARCHAR(50), 
ville VARCHAR(50), 
province VARCHAR(50), 
codePostal VARCHAR(10),  
PRIMARY KEY(AdresseID));

CREATE TABLE Location_Client
(LocationID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
CodeCopieID INTEGER, 
ClientID INTEGER, 
dateLocation TIMESTAMP, 
dateRetour TIMESTAMP,  
PRIMARY KEY(LocationID));

CREATE TABLE Film
(FilmID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
annee INTEGER, 
titre VARCHAR(100), 
pays VARCHAR(50), 
langueOriginale VARCHAR(50), 
genres VARCHAR(1000), 
resumeFilm VARCHAR(4000), 
dureeMinutes INTEGER,
PRIMARY KEY(FilmID));

ALTER TABLE Client
ADD CONSTRAINT FK_AdresseClient
FOREIGN KEY (AdresseID) REFERENCES Adresse(AdresseID);

ALTER TABLE Client
ADD CONSTRAINT FK_ForfaitClient
FOREIGN KEY (ForfaitID) REFERENCES Forfait(ForfaitID);

ALTER TABLE Client
ADD CONSTRAINT FK_PersonneClient
FOREIGN KEY (PersonneID) REFERENCES Personne(PersonneID);

ALTER TABLE Client
ADD CONSTRAINT FK_CarteCreditClient
FOREIGN KEY (CarteCreditID) REFERENCES CarteCredit(CarteCreditID);

ALTER TABLE Film_Acteur
ADD CONSTRAINT FK_Acteur_ActeurFilm
FOREIGN KEY (fk_personneID) REFERENCES Personne(PersonneID);

ALTER TABLE Film_Acteur
ADD CONSTRAINT FK_Film_ActeurFilm
FOREIGN KEY (fk_filmID) REFERENCES Film(FilmID);

ALTER TABLE Realisateur
ADD CONSTRAINT FK_RealisateurPersonne
FOREIGN KEY (fk_PersonneID) REFERENCES Personne(PersonneID);

ALTER TABLE Realisateur
ADD CONSTRAINT FK_RealisateurFilm
FOREIGN KEY (fk_FilmID) REFERENCES Film(FilmID);

ALTER TABLE Scenariste
ADD CONSTRAINT FK_ScenaristeFilm
FOREIGN KEY (fk_FilmID) REFERENCES Film(FilmID);


ALTER TABLE Inventaire
ADD CONSTRAINT FK_FilmInventaire
FOREIGN KEY (FilmID) REFERENCES Film(FilmID);

ALTER TABLE Location_Client
ADD CONSTRAINT FK_InventaireLocation_Client
FOREIGN KEY (CodeCopieID) REFERENCES Inventaire(CodeCopieID);

ALTER TABLE Location_Client
ADD CONSTRAINT FK_ClientLocation_Client
FOREIGN KEY (ClientID) REFERENCES Client(ClientID);



/* Note(Marc) : Je construit deja les forfaits a partir des information dans la BD

INSERT INTO Forfait (coutParMois, typeForfait, locationMax, dureeMaxJour) VALUES (5, 'Débutant', 1, 10);
INSERT INTO Forfait (coutParMois, typeForfait, locationMax, dureeMaxJour) VALUES (10, 'Intermédiaire', 5, 30);
INSERT INTO Forfait (coutParMois, typeForfait, locationMax, dureeMaxJour) VALUES (15, 'Avencé', 10, NULL);
*/
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
	SELECT COUNT(*) INTO EstLoue FROM Location_Client WHERE CodeCopieID = :NEW.CodeCopieID AND dateRetour IS NOT NULL;
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
	SELECT COUNT(*) INTO NBFilm FROM Location_Client WHERE CLIENTID = :NEW.CLIENTID AND dateRetour IS NOT NULL;
	SELECT LocationMax INTO NBFilmMax FROM Client INNER JOIN forfait ON Client.FORFAITID = Forfait.FORFAITID WHERE Client.CLIENTID = :NEW.CLIENTID;
	IF NBFilm > NBFilmMax THEN
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
    
    dbms_output.put_line(adresseID);
EXCEPTION 
WHEN OTHERS THEN raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
END pCreerClient;

/

/* Not tested */
create or replace PROCEDURE pLouerFilm
    (filmID_in IN NUMBER, clientID_in IN NUMBER)
IS
    copieALouer NUMBER;
BEGIN
    SELECT codeCopieID INTO copieALouer
    FROM (
        SELECT *
        FROM Location_Client
        WHERE 
            codeCopieID = (
                SELECT codeCopieID
                FROM Inventaire
                WHERE filmID = filmID_in
            )
            AND dateRetour IS NOT NULL
        ORDER BY dateLocation DESC
    )
    WHERE ROWNUM = 1;
    
    INSERT INTO Location_Client (codeCopieID, clientID, dateLocation)
    VALUES (copieALouer, clientID_in, TO_CHAR(SYSDATE, 'YYYY-MM-DD'));
EXCEPTION 
WHEN OTHERS THEN raise_application_error(-20001,'Erreur lors de la location du film ' || filmID_in);
END pLouerFilm;
/
