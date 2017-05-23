using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


using System.Xml;
using System.IO;
using System.Text;



namespace LOG660_InsertionProgram
{


    class Program
    {
        static void Main(string[] args)
        {
            List<XMLClientData> xml_clients_data = new List<XMLClientData>();
            List<XMLFilmData> xml_film_data = new List<XMLFilmData>();
            List<XMLPersonneData> xml_personne_data = new List<XMLPersonneData>();



           // OSQLConnection my_connection = new OSQLConnection();


            //Base file './' is in the debug folder
            //FileStream xml_client_file = File.Open("./clients_latin1.xml",FileMode.Open);
        //    FileStream xml_film_file = File.Open("./films_latin1.xml", FileMode.Open);
            FileStream xml_personne_film = File.Open("./clients_latin1.xml", FileMode.Open);

            XmlReader r = XmlReader.Create(xml_personne_film);
            
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                   // Console.WriteLine();
                    Console.Write("<" + r.Name + ">");
                    if (r.HasAttributes)
                    {
                        for (int i = 0; i < r.AttributeCount; i++)
                        {
                            Console.WriteLine("\tATTRIBUTE: " + r.GetAttribute(i));
                        }
                    }
                }
                else if (r.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine("\tVALUE: " + r.Value);
                }
            }

            System.Console.ReadKey();

        }
    }
}
