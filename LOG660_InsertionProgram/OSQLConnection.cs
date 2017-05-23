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
        public OSQLConnection()
        {
            string oradb = "Data Source=(DESCRIPTION =" +
                   "(ADDRESS = (PROTOCOL = TCP)(HOST = big-data-3.logti.etsmtl.ca)(PORT = 1521))" +
                   "(CONNECT_DATA =" +
                   "(SERVER = DEDICATED)" +
                   "(SID = LOG660)));" +
                   "User Id=equipe4;Password=wkIrnP7g;";

            OracleConnection conn = new OracleConnection(oradb); // C#
          
          //  conn.ConnectionString = @"Data Source=big-data-3.logti.etsmtl.ca;User Id=equipe39;Password=wkIrnP7g;"; 
            conn.Open();

            Console.WriteLine("Connected to Oracle" + conn.ServerVersion);

            string inset_test = @"insert into Film_Info (fk_RealisateurID,annee,titre,pays,langue_original,genre,film_resume,scenarisme,duree) 
             VALUES(1,2000,'Le retour de la putine','Cananada','Francais','Drole en Criss','This great movies is about this !','Mr.Scenarisme',130)";

           

            OracleCommand cmd = new OracleCommand(inset_test, conn);
            cmd.CommandType = CommandType.Text;
          //  cmd.ExecuteReader();
            OracleDataReader dr = cmd.ExecuteReader();

            bool result =  dr.Read();
            bool result1 = dr.Read();
         
            int test = dr.GetInt32(0);


            conn.Dispose();
        }
    }
}
    