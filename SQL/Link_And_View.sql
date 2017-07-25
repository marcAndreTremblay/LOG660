
-- Get specific cote for 'x' movie and 'y' client
    --select COTE from Proxy_Data_Table where IDFILM = 47478 and IDCLIENT = 913554;

-- Get specific movie average cote value
 -- select (sum(COTE)/count(IDFILM)) from Proxy_Data_Table where IDFILM = 47478;    
-- Retrive all client that already coted both movie  

-- Upper therme
    select   
        sum((tt1.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = 47478) )*
         (tt2.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = 66011)))
     from 
        (select * from Proxy_Data_Table where IDFILM = 47478) tt1,
        (select * from Proxy_Data_Table where IDFILM = 66011) tt2
    where tt1.IDCLIENT = tt2.IDClIENT;



select FILMID,
    -- upper therme
     (select   
        sum((tt1.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = f_j.FILMID) )*
         (tt2.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = 66011)))
     from 
        (select * from Proxy_Data_Table where IDFILM = f_j.FILMID) tt1,
        (select * from Proxy_Data_Table where IDFILM = 66011) tt2
    where tt1.IDCLIENT = tt2.IDClIENT) as value_s1,
    -- first lower therme
    (select   
    (sum(Power((cote - (select sum(cote)/count(IDCLIENT) 
        from  Proxy_Data_Table 
     where IDFILM = f_j.FILMID)),2))) from 
    (select * from Proxy_Data_Table where IDFILM = f_j.FILMID ) ) as value_s2,
    -- secound lower therme
    (select   
    (sum(Power((cote - (select sum(cote)/count(IDCLIENT) 
        from  Proxy_Data_Table 
     where IDFILM = 66011)),2)))  from 
    (select * from Proxy_Data_Table where IDFILM = 66011 )) as value_s3
from FILM f_j;




select FILMID,
    -- upper therme
     (select   
        sum((tt1.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = f_j.FILMID) )*
         (tt2.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = 66011)))
     from 
        (select * from Proxy_Data_Table where IDFILM = f_j.FILMID) tt1,
        (select * from Proxy_Data_Table where IDFILM = 66011) tt2
    where tt1.IDCLIENT = tt2.IDClIENT) / SQRT((
    -- first lower therme
    (select   
    (sum(Power((cote - (select sum(cote)/count(IDCLIENT) 
        from  Proxy_Data_Table 
     where IDFILM = f_j.FILMID)),2))) from 
    (select * from Proxy_Data_Table where IDFILM = f_j.FILMID ) )) *(
    -- secound lower therme
    (select   
    (sum(Power((cote - (select sum(cote)/count(IDCLIENT) 
        from  Proxy_Data_Table 
     where IDFILM = 66011)),2)))  from 
    (select * from Proxy_Data_Table where IDFILM = 66011 )) )) as Correlation_value
from FILM f_j;



-- This is the super querry that will calcule the correlation of every movie again all movie
select f_j.FILMID,f_k.FILMID,
    -- upper therme
     (select   
        sum((tt1.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = f_j.FILMID) )*
         (tt2.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = f_k.FILMID)))
     from 
        (select * from Proxy_Data_Table where IDFILM = f_j.FILMID) tt1,
        (select * from Proxy_Data_Table where IDFILM = f_k.FILMID) tt2
    where tt1.IDCLIENT = tt2.IDClIENT) / SQRT((
    -- first lower therme
    (select   
    (sum(Power((cote - (select sum(cote)/count(IDCLIENT) 
        from  Proxy_Data_Table 
     where IDFILM = f_j.FILMID)),2))) from 
    (select * from Proxy_Data_Table where IDFILM = f_j.FILMID ) )) *(
    -- secound lower therme
    (select   
    (sum(Power((cote - (select sum(cote)/count(IDCLIENT) 
        from  Proxy_Data_Table 
     where IDFILM = f_k.FILMID)),2)))  from 
    (select * from Proxy_Data_Table where IDFILM = f_k.FILMID )) )) as Correlation_value
from (select FILMID from FILM) f_k ,FILM f_j;



drop MATERIALIZED VIEW ma_vue_moyenne;
create MATERIALIZED VIEW ma_vue_moyenne
REFRESH FORCE START WITH SYSDATE
NEXT TRUNC(SYSDATE) + 8 
AS select IDFILM as Id_Movie,(sum(COTE)/count(IDFILM)) as Average from Proxy_Data_Table group by IDFILM;


drop MATERIALIZED VIEW ma_vue_correlation;
--create MATERIALIZED VIEW ma_vue_correlation
--REFRESH FORCE START WITH SYSDATE
--NEXT TRUNC(SYSDATE) + 15 
create  VIEW ma_vue_correlation_not
AS select FILMID,
    -- upper therme
     (select   
        sum((tt1.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = f_j.FILMID) )*
         (tt2.cote - (select sum(cote)/count(IDCLIENT) 
            from  Proxy_Data_Table 
         where IDFILM = 66011)))
     from 
        (select * from Proxy_Data_Table where IDFILM = f_j.FILMID) tt1,
        (select * from Proxy_Data_Table where IDFILM = 66011) tt2
    where tt1.IDCLIENT = tt2.IDClIENT) / SQRT((
    -- first lower therme
    (select   
    (sum(Power((cote - (select sum(cote)/count(IDCLIENT) 
        from  Proxy_Data_Table 
     where IDFILM = f_j.FILMID)),2))) from 
    (select * from Proxy_Data_Table where IDFILM = f_j.FILMID ) )) *(
    -- secound lower therme
    (select   
    (sum(Power((cote - (select sum(cote)/count(IDCLIENT) 
        from  Proxy_Data_Table 
     where IDFILM = 66011)),2)))  from 
    (select * from Proxy_Data_Table where IDFILM = 66011 )) )) as Correlation_value
from FILM f_j;