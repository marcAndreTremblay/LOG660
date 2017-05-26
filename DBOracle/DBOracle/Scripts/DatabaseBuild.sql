

CREATE TABLE PERSONNE(
PersonneID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
prenom VARCHAR(50),
nomFamille VARCHAR(50),
dateNaissance DATE,
PRIMARY KEY(PersonneID));

CREATE TABLE Realisateur
(RealisateurID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1),
 PersonneID INTEGER, 
 biographie VARCHAR(1000), 
 lieuNaissance VARCHAR(50),
  PRIMARY KEY(RealisateurID));

CREATE TABLE Acteur
(ActeurID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
PersonneID INTEGER, 
biographie VARCHAR(1000), 
lieuNaissance VARCHAR(50),
 PRIMARY KEY(ActeurID));

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
numero iNTEGER, 
dateExpiration DATE, 
cvv INTEGER,  
PRIMARY KEY(CarteCreditID));

CREATE TABLE Forfait
(ForfaitID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
coutParMois INTEGER, 
typeForfait VARCHAR(15), 
locationMax INTEGER, 
dureeMaxJour INTEGER,  
PRIMARY KEY(ForfaitID));

CREATE TABLE Film_Acteur
(ActeurID INTEGER GENERATED ALWAYS as IDENTITY(START with 1 INCREMENT by 1), 
FilmID INTEGER, 
personnage VARCHAR(50),
PRIMARY KEY(ActeurID, FilmID));

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
RealisateurID INTEGER,
annee INTEGER, 
titre VARCHAR(100), 
pays VARCHAR(50), 
langueOriginale VARCHAR(50), 
genres VARCHAR(1000), 
resumeFilm VARCHAR(4000), 
scenariste VARCHAR(1000), 
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
FOREIGN KEY (ActeurID) REFERENCES Acteur(ActeurID);

ALTER TABLE Film_Acteur
ADD CONSTRAINT FK_Film_ActeurFilm
FOREIGN KEY (FilmID) REFERENCES Film(FilmID);

ALTER TABLE Realisateur
ADD CONSTRAINT FK_PersonneRealisateur
FOREIGN KEY (PersonneID) REFERENCES Personne(PersonneID);

ALTER TABLE Acteur
ADD CONSTRAINT FK_PersonneActeur
FOREIGN KEY (PersonneID) REFERENCES Personne(PersonneID);

ALTER TABLE Inventaire
ADD CONSTRAINT FK_FilmInventaire
FOREIGN KEY (FilmID) REFERENCES Film(FilmID);

ALTER TABLE Location_Client
ADD CONSTRAINT FK_InventaireLocation_Client
FOREIGN KEY (CodeCopieID) REFERENCES Inventaire(CodeCopieID);

ALTER TABLE Location_Client
ADD CONSTRAINT FK_ClientLocation_Client
FOREIGN KEY (ClientID) REFERENCES Client(ClientID);

ALTER TABLE FILM
ADD CONSTRAINT FK_RealisateurFilm
FOREIGN KEY (RealisateurID) REFERENCES Realisateur(RealisateurID);

--------------------------------------------------------------------------------------------------------------------
--                                               Les proc�dures
--------------------------------------------------------------------------------------------------------------------
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
/


CREATE OR REPLACE TRIGGER VerifierAgeClient
BEFORE INSERT ON Client
FOR EACH ROW
BEGIN
	IF (SELECT DateNaissance FROM Personne WHERE PersonneID = :NEW.PersonneID) < ADD_MONTH(SYSDATE, -216) THEN --le select marche pas
		DELETE FROM Personne WHERE PersonneID = :NEW.PersonneID;
		RAISE_APPLICATION_ERROR('-20000', 'Le client doit avoir au moins 18 ans');
	END IF;
END;
/
