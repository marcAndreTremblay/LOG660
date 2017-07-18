CREATE DATABASE LINK  test_link
CONNECT TO EXT660E IDENTIFIED BY "LoG660x" USING 'EXT660';

CREATE SYNONYM Proxy_Data_Table
FOR LOG6601C.COTES@test_link;

-- Get specific cote for 'x' movie and 'y' client
    --select COTE from Proxy_Data_Table where IDFILM = 47478 and IDCLIENT = 913554;

-- Get specific movie average cote value
 -- select (sum(COTE)/count(IDFILM)) from Proxy_Data_Table where IDFILM = 47478;    
-- Retrive all client that already coted both movie  
 -- select count(IDCLIENT) from Proxy_Data_Table where IDFILM = 47478 or IDFILM = 66011;


drop MATERIALIZED VIEW ma_vue_moyenne;
create MATERIALIZED VIEW ma_vue_moyenne
REFRESH FORCE START WITH SYSDATE
NEXT TRUNC(SYSDATE) + 8 
AS select IDFILM as Id_Movie,(sum(COTE)/count(IDFILM)) as Average from Proxy_Data_Table group by IDFILM;