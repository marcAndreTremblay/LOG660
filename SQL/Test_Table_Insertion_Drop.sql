drop TABLE Film_Info;

CREATE TABLE Film_Info (
    pk_Id INT GENERATED ALWAYS AS IDENTITY, 
    fk_RealisateurID INT , 
    annee INT,
    titre varchar(100),
    pays varchar(30),
    langue_original varchar(30),
    genre varchar(20),
    film_resume varchar(200),
    scenarisme varchar(20),
    duree INT
);
insert into Film_Info (fk_RealisateurID,annee,titre,pays,langue_original,genre,film_resume,scenarisme,duree) 
             VALUES(1,2000,'Le retour de la putine','Cananada','Francais','Drole en Criss','This great movies is about this !','Mr.Scenarisme',130);
select * from Film_Info;