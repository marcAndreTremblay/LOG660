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
                   "User Id=equipe39;Password=wkIrnP7g;";

            OracleConnection conn = new OracleConnection(oradb); // C#
          
          //  conn.ConnectionString = @"Data Source=big-data-3.logti.etsmtl.ca;User Id=equipe39;Password=wkIrnP7g;"; 
            conn.Open();

            Console.WriteLine("Connected to Oracle" + conn.ServerVersion);


            OracleCommand cmd = new OracleCommand();

            cmd.Connection = conn;

           
            cmd.CommandText = @"CREATE TABLE my_test_table (t_id INT PRIMARY KEY, 
                                                            t_c2 INT, 
                                                            t_c3 INT
                                                        );";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();

            dr.Read();

            System.Console.WriteLine(dr.GetString(0));

           

            conn.Dispose();
        }
    }
}
