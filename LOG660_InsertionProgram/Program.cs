using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System;
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

            List<string> forfait_list = new List<string>();

       //    OSQLConnection my_connection = new OSQLConnection();



            //Base file './' is in the debug folder
            //FileStream xml_client_file = File.Open("./clients_latin1.xml",FileMode.Open);
        //    FileStream xml_film_file = File.Open("./films_latin1.xml", FileMode.Open);
            FileStream xml_personne_film = File.Open("./personnes_latin1.xml", FileMode.Open);

            XmlReader r = XmlReader.Create(xml_personne_film);

            XMLPersonneData c_p_data = null;

            string c_node_name = "";
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    c_node_name = r.Name;
                    switch (c_node_name)
                    {
                        case "personne":
                            {
                                if(c_p_data != null)
                                {
                                   xml_personne_data.Add(c_p_data);
                                   c_p_data = null;
                                   c_p_data = new XMLPersonneData();
                                }
                                else
                                {
                                    c_p_data = new XMLPersonneData();
                                    Console.WriteLine("<" + c_node_name + ">");
                                }
                                if (r.HasAttributes)
                                {
                                    c_p_data.xml_id = System.Convert.ToInt32(r.GetAttribute(0));
                                }
                                break;
                            }
                        
                        default: {
                                Console.WriteLine("<" + c_node_name + ">");
                               
                                bool result = r.Read();
                                if (result == true)
                                {
                                    if (r.NodeType == XmlNodeType.Text)
                                    {
                                        if(c_node_name == "nom")
                                        {
                                            c_p_data.name = r.Value;
                                            Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "bio")
                                        {
                                            c_p_data.biographie = r.Value;
                                            Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "anniversaire")
                                        {
                                            c_p_data.naissance_info.data = r.Value;
                                            Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "lieu")
                                        {
                                            c_p_data.naissance_info.lieu = r.Value;
                                            Console.WriteLine("\tVALUE: " + r.Value);
                                        }

                                    }

                                }
                                else
                                {
                                    r.Read();
                                }
                                break; }
                    } 
                }
            }
            

            System.Console.ReadKey();



        }
    }
}
