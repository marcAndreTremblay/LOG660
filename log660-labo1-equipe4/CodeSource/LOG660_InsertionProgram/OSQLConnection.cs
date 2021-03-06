﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace LOG660_InsertionProgram
{
    public class OSQLConnection
    {
        OracleConnection m_connection;
       
        public void InsertRealisateur(int ref_personne_id, int ref_film_id)
        {
            string cmd_string = @"INSERT INTO REALISATEUR(fk_FilmID,fk_PersonneID)
                                                VALUES(" + ref_film_id + ","+ ref_personne_id + ")";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;
        }

        public void InsertInvetaireCopy(int  ref_film_id)
        {

            string cmd_string = @"INSERT INTO INVENTAIRE(FILMID)
                                                VALUES(" + ref_film_id + ")";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;
        }
        public void InsertRole(int fk_ref_film,XMLRoleData role)
        {
            string cmd_string = @"INSERT INTO FILM_ACTEUR (FK_PERSONNEID,FK_FILMID,PERSONNAGE) 
                                                VALUES(" + role.ref_personne_bd_id+"," + fk_ref_film + ",'" + role.personnage_name + "')";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;
        }
        public void InsertSceneriste(int fk_ref_film, string scenariste_name)
        {

            string cmd_string = @"INSERT INTO SCENARISTE (FK_FILMID,NOM) 
                                                VALUES("+ fk_ref_film + ",'"+scenariste_name+"')";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;
        }
        public void InsertFilm(XMLFilmData c_data)
        {
            string genre_concatenation = "";
            foreach(string c_s in c_data.genre_list)
            {
                genre_concatenation += c_s + ",";
            }

            string cmd_string = @"INSERT INTO FILM (ANNEE,TITRE,PAYS,LANGUEORIGINALE,GENRES,RESUMEFILM,DUREEMINUTES) 
                                                VALUES("+ c_data.year + ",'" + c_data.title + "','"
                                                        + c_data.pays + "','" + c_data.langue + "','"
                                                        + genre_concatenation+"','"+c_data.resumer+"',"+c_data.duree+")";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;
        }
        public void InsertClient(XMLClientData c_data)
        {
            //,PASSWORD
            string cmd_string = @"insert into CLIENT ( PERSONNEID,FORFAITID,
                                                      ADRESSEID,CARTECREDITID,
                                                       NUMEROTEL,COURRIEL)  
                                             VALUES ("+c_data.Ref_personne+","+c_data.Ref_Forfait_id+","
                                                     +c_data.Ref_address+","+c_data.Ref_redit_cart
                                                     +",'"+ c_data.telephone+ "','"+c_data.courriel+"')";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;

        }
        public void InsertCarteCredit(string carte_Type,string number,int exp_month,int exp_year,int csv)
        {
            string cmd_string = @"INSERT INTO CARTECREDIT (TYPECARTE,NUMERO,exp_month,exp_year,CVV)
                                    VALUES ('"+ carte_Type + "','"+ number + "',"+ exp_month + ","+ exp_year + ","+ csv + ")";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;

        }
        public void InserADress(int num_civic , string rue,string ville,string province,string code_postal)
        {
            string cmd_string = @"insert into EQUIPE4.ADRESSE ( NOCIVIQUE,RUE,VILLE,PROVINCE,CODEPOSTAL) 
                                             values ("+ num_civic + ", '"+ rue + "','"+ ville + "','"+ province + "','"+ code_postal + "')";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;

        }
        public void InsertForfait(ForfaitData data)
        {
            string cmd_string = @"insert into EQUIPE4.FORFAIT (COUTPARMOIS,TYPEFORFAIT,LOCATIONMAX,DUREEMAXJOUR) 
                  values ( " + data.coupPerMount + ", '" + data.Nom + "'," + data.location_max + "," + data.location_duree + ")";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteReader();
            cmd = null;

        }
            public void InsertPersonne(XMLPersonneData data)
        {
            
            string cmd_string = @"insert into EQUIPE4.PERSONNE (PRENOM,NOMFAMILLE,DATENAISSANCE,BIOGRAPHIE,LIEUNAISSANCE) 
                  values ( '" + data.name + "', '" + data.last_name + "','"+ data.naissance_info.data + "','" + "','" + data.naissance_info.lieu + "')";

            OracleCommand cmd = new OracleCommand(cmd_string, m_connection);
           
            cmd.CommandType = CommandType.Text;
           cmd.ExecuteReader();
            cmd = null;


        }
        public OSQLConnection()
        {
            string oradb = "Data Source=(DESCRIPTION =" +
                   "(ADDRESS = (PROTOCOL = TCP)(HOST = big-data-3.logti.etsmtl.ca)(PORT = 1521))" +
                   "(CONNECT_DATA =" +
                   "(SERVER = DEDICATED)" +
                   "(SID = LOG660)));" +
                   "User Id=equipe4;Password=wkIrnP7g;";

            m_connection = new OracleConnection(oradb); // C#
            m_connection.Open();
            //  conn.ConnectionString = @"Data Source=big-data-3.logti.etsmtl.ca;User Id=equipe39;Password=wkIrnP7g;"; 


            //Console.WriteLine("Connected to Oracle" + m_connection.ServerVersion);







            //Step 1 -> add a personne and retrive the last id created
            //string personne_insertion = @"insert into EQUIPE4.PERSONNE (PERSONNEID, PRENOM,NOMFAMILLE,DATENAISSANCE) 
            //         values ('"+ "', '" + "', '" + "','" + "');";

            ////Step 2 -> Create a credit card instance and retrive the last id created
            //string credit_card_insertion = @"insert into EQUIPE4.CARTECREDIT (CARTECREDITID, TYPECARTE,NUMERO,DATEEXPIRATION,CVV) 
            //         values ('" + "', '" + "','" + "','" + "','" + "');";

            ////Step 3 -> Create a adress instance and retrive the last id created
            //string adress_insertion = @"insert into EQUIPE4.ADRESSE (ADRESSEID, NOCIVIQUE,RUE,VILLE,PROVINCE,CODEPOSTAL) 
            //            values ('" + "', '" + "','" + "','" + "','" + "','" + "'); ";


            ////Step 4 with ids in step 1,2,3 create a client instance
            //string client_insertion = @"insert into EQUIPE4.CLIENT (CLIENTID, PERSONNEID,FORFAITID,ADRESSEID,CARTECREDITID,NUMEROTEL,COURRIEL,PASSWORD) ";



            //string person2ne_insertion = @"insert into EQUIPE4.PERSONNE (PRENOM,NOMFAMILLE,DATENAISSANCE) 
            //         values ('Test_from_prog', 'Test_from_prog','1992-06-23')";

            //  OracleCommand cmd = new OracleCommand(person2ne_insertion, m_connection);
            //  cmd.CommandType = CommandType.Text;
            ////  cmd.ExecuteReader();
            //  OracleDataReader dr = cmd.ExecuteReader();

            //  bool result =  dr.Read();
            //  bool result1 = dr.Read();

            //  //int test = dr.GetInt32(0);


            //  m_connection.Dispose();
        }
    }
}
    