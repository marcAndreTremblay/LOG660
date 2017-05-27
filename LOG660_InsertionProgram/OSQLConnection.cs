using System;
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



        public void InsertPersonne(XMLPersonneData data)
        {
            string cmd_string = @"insert into EQUIPE4.PERSONNE ( PRENOM,NOMFAMILLE,DATENAISSANCE) 
                  values ( '"+ data.name + "', '" + data.last_name + "','"+ data.naissance_info.data + "')";
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

            //  conn.ConnectionString = @"Data Source=big-data-3.logti.etsmtl.ca;User Id=equipe39;Password=wkIrnP7g;"; 
            m_connection.Open();

            //Console.WriteLine("Connected to Oracle" + m_connection.ServerVersion);






 
            //Step 1 -> add a personne and retrive the last id created
            string personne_insertion = @"insert into EQUIPE4.PERSONNE (PERSONNEID, PRENOM,NOMFAMILLE,DATENAISSANCE) 
                     values ('"+ "', '" + "', '" + "','" + "');";

            //Step 2 -> Create a credit card instance and retrive the last id created
            string credit_card_insertion = @"insert into EQUIPE4.CARTECREDIT (CARTECREDITID, TYPECARTE,NUMERO,DATEEXPIRATION,CVV) 
                     values ('" + "', '" + "','" + "','" + "','" + "');";

            //Step 3 -> Create a adress instance and retrive the last id created
            string adress_insertion = @"insert into EQUIPE4.ADRESSE (ADRESSEID, NOCIVIQUE,RUE,VILLE,PROVINCE,CODEPOSTAL) 
                        values ('" + "', '" + "','" + "','" + "','" + "','" + "'); ";


            //Step 4 with ids in step 1,2,3 create a client instance
            string client_insertion = @"insert into EQUIPE4.CLIENT (CLIENTID, PERSONNEID,FORFAITID,ADRESSEID,CARTECREDITID,NUMEROTEL,COURRIEL,PASSWORD) ";



            string person2ne_insertion = @"insert into EQUIPE4.PERSONNE (PRENOM,NOMFAMILLE,DATENAISSANCE) 
                     values ('Test_from_prog', 'Test_from_prog','1992-06-23')";

            OracleCommand cmd = new OracleCommand(person2ne_insertion, m_connection);
            cmd.CommandType = CommandType.Text;
          //  cmd.ExecuteReader();
            OracleDataReader dr = cmd.ExecuteReader();

            bool result =  dr.Read();
            bool result1 = dr.Read();
         
            //int test = dr.GetInt32(0);


            m_connection.Dispose();
        }
    }
}
    