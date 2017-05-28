CREATE TABLE PERSONNE(
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
CHECK(password like '^[a-zA-Z0-9]{5,}$'));

CREATE TABLE Employe
(EmployeID iNTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
PersonneID INTEGER, 
AdresseID INTEGER, 
numeroTel VARCHAR(20), 
courriel VARCHAR(50) UNIQUE, 
password VARCHAR(100), 
matricule VARCHAR(7),  
PRIMARY KEY(EmployeID),
CHECK(password like '^[a-zA-Z0-9]{5,}$'));

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
EmployeID INTEGER, 
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




/*
INSERT INTO Forfait (coutParMois, typeForfait, locationMax, dureeMaxJour) VALUES (5, 'D�butant', 1, 10);
INSERT INTO Forfait (coutParMois, typeForfait, locationMax, dureeMaxJour) VALUES (10, 'Interm�diaire', 5, 30);
INSERT INTO Forfait (coutParMois, typeForfait, locationMax, dureeMaxJour) VALUES (15, 'Avenc�', 10, NULL);

CREATE OR REPLACE TRIGGER VerifierDateExpirationCarte --test�
BEFORE INSERT ON CarteCredit
FOR EACH ROW
BEGIN
	IF :NEW.DateExpiration <= trunc(sysdate) THEN
		RAISE_APPLICATION_ERROR('-20000', 'Carte de cr�dit expir�');
	END IF;
END;



CREATE OR REPLACE TRIGGER VerifierAgeClient
BEFORE INSERT ON Client
FOR EACH ROW
BEGIN
	IF (SELECT DateNaissance FROM Personne WHERE PersonneID = :NEW.PersonneID) < ADD_MONTH(SYSDATE, -216) THEN --le select marche pas
		DELETE FROM Personne WHERE PersonneID = :NEW.PersonneID;
		RAISE_APPLICATION_ERROR('-20000', 'Le client doit avoir au moins 18 ans');
	END IF;
END;


CREATE OR REPLACE TRIGGER VerifierSiLocationDisponible
BEFORE INSERT ON Location_Client
FOR EACH ROW
BEGIN
	IF (SELECT * FROM Location_Client WHERE CodeCopieID = :NEW.CodeCopieID AND dateRetour IS NOT NULL) EXISTS THEN
		RAISE_APPLICATION_ERROR('-20000', 'La copie doit �tre disponible pour pouvoir la louer');
	END IF;
END;


CREATE OR REPLACE TRIGGER VerifierSiClientLouePlusQueMax
BEFORE INSERT ON Location_Client
FOR EACH ROW
BEGIN
	IF (SELECT COUNT(*) FROM Location_Client WHERE CLIENTID = :NEW.CLIENTID AND dateRetour IS NOT NULL) > (SELECT LocationMax FROM Client INNER JOIN forfait ON Client.FORFAITID = Forfait.FORFAITID) THEN
		RAISE_APPLICATION_ERROR('-20000', 'Le client ne peut pas avoir plus de location que son forfait lui permet');
	END IF;
END;
*/