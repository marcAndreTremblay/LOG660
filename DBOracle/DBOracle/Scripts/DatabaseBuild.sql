
DROP TABLE personne;


CREATE TABLE PERSONNE(
PersonneID INTEGER,
prenom VARCHAR(50),
nomFamille VARCHAR(50),
dateNaissance DATE,
PRIMARY KEY(PersonneID))

CREATE TABLE Realisateur
(RealisateurID INTEGER, PersonneID INTEGER, biographie VARCHAR(MAX), lieuNaissance VARCHAR(50), PRIMARY KEY(RealisateurID))

CREATE TABLE Acteur
(ActeurID INTEGER, PersonneID INTEGER, biographie VARCHAR(MAX), lieuNaissance VARCHAR(50), PRIMARY KEY(ActeurID))

CREATE TABLE Client
(ClientID INTEGER, PersonneID INTEGER, ForfaitID INTEGER, AdresseID INTEGER, CarteCreditID, INTEGER, numeroTel VARCHAR(20), courriel VARCHAR(50), password VARCHAR(100),  PRIMARY KEY(ClientID))

CREATE TABLE Employe
(EmployeID iNTEGER, PersonneID INTEGER, AdresseID INTEGER, numeroTel VARCHAR(20), courriel VARCHAR(50), password VARCHAR(100), matricule VARCHAR(7),  PRIMARY KEY(EmployeID))

CREATE TABLE CarteCredit
(CarteCreditID INTEGER, typeCarte VARCHAR(10), numero iNTEGER, dateExpiration DATE, cvv INTEGER,  PRIMARY KEY(CarteCreditID))

CREATE TABLE Forfait
(ForfaitID INTEGER, coutParMois INTEGER, typeForfait VARCHAR(15), locationMax INTEGER, dureeMax TIMESTAMP,  PRIMARY KEY(ForfaitID))

CREATE TABLE Film_Acteur
(ActeurID INTEGER, FilmID INTEGER, personnage VARCHAR)

CREATE TABLE Inventaire
(CodeCopieID INTEGER, FilmID INTEGER,  PRIMARY KEY(CodeCopieID))

CREATE TABLE Adresse
(AdresseID INTEGER, noCivique VARCHAR, rue VARCHAR, ville VARCHAR, province VARCHAR, codePostal VARCHAR,  PRIMARY KEY(AdresseID))

CREATE TABLE Location_Client
(LocationID INTEGER, CodeCopieID INTEGER, ClientID INTEGER, EmployeID INTEGER, dateLocation TIMESTAMP, dateRetour TIMESTAMP,  PRIMARY KEY(LocationID))

