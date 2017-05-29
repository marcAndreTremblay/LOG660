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

using System.Threading; 

namespace LOG660_InsertionProgram
{


    /*
       This is the synchronisation diagram for the main app

       0                    1                           2                                   3                          4
       |                    |->------ Load XML 1 ------>|                                   |                          |
       |->----- Main ------>|->------ Load XML 2 ------>|->------- Data pre-process*------->|->----Insert in DB ------>|
       |                    |->------ Load XML 3 ------>|                                   |                          |
       |                    |->------------------------Rebuild BD-------------------------->|                          |

        Sync timing : 1,2,3,4
        Data pre-process* :
            Gen all aditional information we will need to the database insertion
            Including :
                -Dictionary for old_xml_id to bd_id for fast seach
                -Sorting List of Data by old_xml_id ,when inserting better data layout with lagacy data
                -Parsing client for type of Forfait
       */

    class Program
    {
        static int SeachKey(int[,] dic,int key , int size)
        {
            int row_size = dic.Length / 2;
            for (int i = 0; i < row_size; i++)
            {
                if(dic[i,0] == key)
                {
                    return dic[i, 1];
                }
            }
            return -1; //Return error
        }

        static void FetchClientData(FileStream xml_file,
                                    List<XMLClientData> list_data)
        {
            Console.WriteLine("Fetching client data ");


            XmlReader r = XmlReader.Create(xml_file);
            XMLClientData c_c_data = null;
            String c_node_name = "";
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    c_node_name = r.Name;
                    switch (c_node_name)
                    {
                        case "client": {
                                //Console.WriteLine("<" + c_node_name + ">");
                                if (c_c_data != null)
                                {
                                    list_data.Add(c_c_data);
                                    c_c_data = null;
                                    c_c_data = new XMLClientData();
                                   
                                }
                                else
                                {
                                    c_c_data = new XMLClientData();

                                }
                                if (r.HasAttributes)
                                {
                                    c_c_data.xml_id = System.Convert.ToInt32(r.GetAttribute(0));
                                    //Console.WriteLine("\t Xml Id : " + r.GetAttribute(0));

                                }
                                break; }
                        default: {

                                //Console.WriteLine("<" + c_node_name + ">");

                                bool result = r.Read();
                                if (result == true)
                                {
                                    if (r.NodeType == XmlNodeType.Text)
                                    {
                                        if (c_node_name == "nom-famille")
                                        {
                                            c_c_data.last_name = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "prenom")
                                        {
                                            c_c_data.first_name = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "courriel")
                                        {
                                            c_c_data.courriel = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "anniversaire")
                                        {
                                            c_c_data.aniversaire = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "adresse")
                                        {
                                            c_c_data.address = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "ville")
                                        {
                                            c_c_data.ville = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "province")
                                        {
                                            c_c_data.province = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "code-postal")
                                        {
                                            c_c_data.code_postal = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }

                                        if (c_node_name == "carte")
                                        {
                                            c_c_data.credit_carte_indo.carte_type = r.Value;
                                            //Console.WriteLine("\t\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "no")
                                        {
                                            c_c_data.credit_carte_indo.No = r.Value;
                                            //Console.WriteLine("\t\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "exp-mois")
                                        {
                                            c_c_data.credit_carte_indo.exp_mount = System.Convert.ToInt32(r.Value);
                                            //Console.WriteLine("\t\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "exp-annee")
                                        {
                                            c_c_data.credit_carte_indo.exp_year = System.Convert.ToInt32(r.Value);
                                            //Console.WriteLine("\t\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "mot-de-passe")
                                        {
                                            c_c_data.mot_de_passe =r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "forfait")
                                        {
                                            c_c_data.forfait = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
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
            if (c_c_data != null)
            {
                list_data.Add(c_c_data);
            }
            Console.WriteLine("Fetching clients data completed");
        }

        static void FetchFilmData(FileStream xml_file,
                                    List<XMLFilmData> list_data)
        {
            Console.WriteLine("Fetching film data ");
            XmlReader r = XmlReader.Create(xml_file);

            XMLFilmData c_f_data = null;
            XMLRoleData curren_role = null;

            String c_node_name = "";
          
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.Element)
                {
                    c_node_name = r.Name;
                    switch (c_node_name)
                    {
                        case "acteur": {
                                if (r.HasAttributes)
                                {
                                    curren_role.xml_personne_id = System.Convert.ToInt32(r.GetAttribute(0));
                                }

                                //Console.WriteLine("\t Attribute: " + c_f_data.realisateur_xml_id);

                                break; }
                        case "role": {
                                if (curren_role == null)
                                {
                                    curren_role = new XMLRoleData();
                                }
                                else
                                {
                                    c_f_data.roles_list.Add(curren_role);
                                    curren_role = null;
                                    curren_role = new XMLRoleData();
                                }

                                break; }
                        case "realisateur":
                            {
                                    if (r.HasAttributes)
                                    {
                                        c_f_data.realisateur_xml_id = System.Convert.ToInt32(r.GetAttribute(0));
                                    }

                                    //Console.WriteLine("\t Attribute: " + c_f_data.realisateur_xml_id);  
                                break;
                            }
                        case "film":
                            {
                                //Console.WriteLine("<" + c_node_name + ">");
                                if (c_f_data != null)
                                {
                                    list_data.Add(c_f_data);
                                    c_f_data = null;
                                    c_f_data = new XMLFilmData();
                                    curren_role = null;
                                }
                                else
                                {
                                    c_f_data = new XMLFilmData();

                                }
                                if (r.HasAttributes)
                                {
                                    c_f_data.xml_id = System.Convert.ToInt32(r.GetAttribute(0));
                                }
                                break;
                            }
                        default:
                            {
                                //Console.WriteLine("<" + c_node_name + ">");

                                bool result = r.Read();
                                if (result == true)
                                {
                                    if (r.NodeType == XmlNodeType.Text)
                                    {
                                        if (c_node_name == "titre")
                                        {
                                            c_f_data.title = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "annee")
                                        {
                                            c_f_data.year = System.Convert.ToInt32(r.Value);
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "pays")
                                        {
                                            c_f_data.pays = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "langue")
                                        {
                                            c_f_data.langue = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "duree")
                                        {
                                            c_f_data.duree = System.Convert.ToInt32(r.Value);
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "resume")
                                        {
                                            c_f_data.resumer = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "genre")
                                        {
                                            c_f_data.genre_list.Add(r.Value);
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "scenariste")
                                        {
                                            c_f_data.scenariste_list.Add(r.Value);
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "personnage")
                                        {
                                            curren_role.personnage_name = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
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
            // Add the last one manualy
            if (curren_role != null)
            {
                c_f_data.roles_list.Add(curren_role);
            }
            //Add the last one manualy
            if (c_f_data != null)
            {
                list_data.Add(c_f_data);
            }
            Console.WriteLine("Fetching film data completed");
        }

       
        static void FetchPresonneData(FileStream xml_personne_film,
                                        List<XMLPersonneData> xml_personne_data)
        {
            Console.WriteLine("Fetching Personne data ");
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
                                //Console.WriteLine("<" + c_node_name + ">");
                                if (c_p_data != null)
                                {
                                    xml_personne_data.Add(c_p_data);
                                    c_p_data = null;
                                    c_p_data = new XMLPersonneData();
                                }
                                else
                                {
                                    c_p_data = new XMLPersonneData();

                                }
                                if (r.HasAttributes)
                                {
                                    c_p_data.xml_id = System.Convert.ToInt32(r.GetAttribute(0));
                                }
                                break;
                            }

                        default:
                            {
                                //Console.WriteLine("<" + c_node_name + ">");

                                bool result = r.Read();
                                if (result == true)
                                {
                                    if (r.NodeType == XmlNodeType.Text)
                                    {
                                        if (c_node_name == "nom")
                                        {
                                            c_p_data.name = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "bio")
                                        {
                                            c_p_data.biographie = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "anniversaire")
                                        {
                                            c_p_data.naissance_info.data = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }
                                        if (c_node_name == "lieu")
                                        {
                                            c_p_data.naissance_info.lieu = r.Value;
                                            //Console.WriteLine("\tVALUE: " + r.Value);
                                        }

                                    }

                                }
                                else
                                {
                                    r.Read();
                                }
                                break;
                            }
                    }
                }
            }
            //Add the last one manualy
            if (c_p_data != null)
            {
                xml_personne_data.Add(c_p_data);
            }
            Console.WriteLine("Fetching personne data completed!");
        }

        static void GenForfaitsFromClientData(List<XMLClientData> in_client_data,
                                              List<ForfaitData> out_forfait_data)
        {
            int id_cpt = 1;
            foreach (XMLClientData c_c_data in in_client_data)
            {
                bool alreadyIn = false;
                ForfaitData found_data = null;
                foreach (ForfaitData c_f in out_forfait_data)
                {
                    if (c_f.Nom == c_c_data.forfait.ToCharArray()[0])
                    {
                        found_data = c_f;
                        alreadyIn = true;
                        break;
                    }
                }
                if (alreadyIn == false)
                {
                    ForfaitData new_data = new ForfaitData();
                    new_data.Nom = c_c_data.forfait.ToCharArray()[0];
                    new_data.id_bd = id_cpt;
                    out_forfait_data.Add(new_data);
                    c_c_data.Ref_Forfait_id = new_data.id_bd;
                    id_cpt++;
                }
                else
                {
                    c_c_data.Ref_Forfait_id = found_data.id_bd;
                }
            }
        }

        static void Main(string[] args)
        {
            Random my_ramdom = new Random((Int32)DateTime.Now.Ticks);

            //Note(Marc): Sync point 0
   
            OSQLConnection my_connection = new OSQLConnection();
            

            // Fetch personne data from the xml personnes_latin1.xml and store them into a list
            FileStream xml_clients_file = File.Open("./clients_latin1.xml", FileMode.Open);
            List<XMLClientData> xml_clients_data = new List<XMLClientData>();
            Thread t1_c = new Thread(() => FetchClientData(xml_clients_file, xml_clients_data));            
            //  End personnes data fetch

            // Fetch personne data from the xml personnes_latin1.xml and store them into a list
            FileStream xml_personne_file = File.Open("./personnes_latin1.xml", FileMode.Open);
            List<XMLPersonneData> xml_personne_data = new List<XMLPersonneData>();
            Thread t2_p = new Thread(() => FetchPresonneData(xml_personne_file, xml_personne_data));
            //End personnes data fetch

            // Fetch personne data from the xml personnes_latin1.xml and store them into a list
            FileStream xml_film_file = File.Open("./films_latin1.xml", FileMode.Open);
            List<XMLFilmData> xml_film_data = new List<XMLFilmData>();
            Thread t3_f = new Thread(() => FetchFilmData(xml_film_file, xml_film_data));
            //End film fetch

           

            long start_time_stamp = DateTime.Now.Millisecond;
            long end_time_stamp = 0;

                //Note(Marc): Sync point 1
            t1_c.Start();
                t2_p.Start();
                t3_f.Start();


                //Note(Marc): Sync point 2
                t1_c.Join();
                t2_p.Join();
                t3_f.Join();

            end_time_stamp = DateTime.Now.Millisecond;
            long xml_load_time = end_time_stamp - start_time_stamp;
         
            //Order the by xml id
            Thread t1_s_c = new Thread(() => xml_clients_data.Sort(XMLClientData.GetComparer()));
            Thread t2_s_p = new Thread(() => xml_personne_data.Sort(XMLPersonneData.GetComparer()));
            Thread t3_s_f = new Thread(() => xml_film_data.Sort(XMLFilmData.GetComparer()));

            start_time_stamp = DateTime.Now.Millisecond;

                //Note(Marc): Sync point 2
                t1_s_c.Start();
                t2_s_p.Start();
                t3_s_f.Start();



                //Note(Marc): Sync point 3
                t1_s_c.Join();
                t2_s_p.Join();
                t3_s_f.Join();

            
            List<ForfaitData> forfait_list = new List<ForfaitData>();

            GenForfaitsFromClientData(xml_clients_data, forfait_list);
            int tempo_c = 10;
            int tempo_d = 2;
            int tempo_l = 2;
            foreach (ForfaitData c_data in forfait_list)
            {
                c_data.coupPerMount = tempo_c;
                c_data.location_duree = tempo_d;
                c_data.location_max = tempo_l;
                tempo_c += 5;
                tempo_d += 2;
                tempo_l++;

            }

                //Build a relation id dictionnary for furtur look up
                int personne_id_cpt = 1;
                int[,] dic_id_relation = new int[xml_personne_data.Count, 2];
                int dic_next_free = 0;
                foreach (XMLPersonneData c_personne in xml_personne_data)
                {
                    c_personne.db_id = personne_id_cpt;
                    dic_id_relation[dic_next_free, 0] = c_personne.xml_id;
                    dic_id_relation[dic_next_free, 1] = c_personne.db_id;
                    personne_id_cpt++;
                    dic_next_free++;
                }

                 
                foreach(XMLFilmData c_f_data in xml_film_data)
                {
                    foreach(XMLRoleData c_r_data in c_f_data.roles_list)
                    {
                        c_r_data.ref_personne_bd_id = SeachKey(dic_id_relation,c_r_data.xml_personne_id, xml_personne_data.Count);
                        if(c_r_data.ref_personne_bd_id == -1)
                        {
                            System.Console.WriteLine("\tFilm : " + c_f_data.xml_id);

                        }
                }
                }

            end_time_stamp = DateTime.Now.Millisecond;
            long data_preprocess_time = end_time_stamp - start_time_stamp;

            long start_time_stamp3 = DateTime.Now.Ticks;
            foreach (ForfaitData c_data in forfait_list)
            {
                my_connection.InsertForfait(c_data);
            }
            foreach (XMLPersonneData c_personne in xml_personne_data)
            {
                my_connection.InsertPersonne(c_personne);
            }
            int tempo_personne = personne_id_cpt;
           
            int tempo_credit_cart = 1;
            int tempo_address = 1;
            foreach (XMLClientData c_data in xml_clients_data)
            {
                //Insert New personne
                XMLPersonneData new_p = new XMLPersonneData();
                new_p.name = c_data.first_name;
                new_p.last_name = c_data.last_name;
                new_p.naissance_info.data = c_data.aniversaire;
                new_p.naissance_info.lieu = "";
                new_p.biographie = "";
                new_p.photo_link = "";
                my_connection.InsertPersonne(new_p);
                c_data.Ref_personne = tempo_personne;// Store pk for fk later
                tempo_personne++;

                string[] adress_split = c_data.address.Split(' ');
                string street_name = "";
                for (int i = 1; i < adress_split.Length; i++)
                {
                    street_name += adress_split[i];
                }
                //Insert Adress
                my_connection.InserADress(Convert.ToInt32(adress_split[0]),
                    street_name, c_data.ville, c_data.province, c_data.code_postal);
                c_data.Ref_address = tempo_address;  // Store pk for fk later
                tempo_address++;

                //Insert Credit card
                my_connection.InsertCarteCredit(c_data.credit_carte_indo.carte_type,
                                              /*Convert.ToInt32(c_data.credit_carte_indo.No.Trim(' '))*/123,
                                              c_data.credit_carte_indo.exp_mount,
                                              c_data.credit_carte_indo.exp_year,
                                              /*Convert.ToInt32(my_ramdom.NextDouble()*999)*/123);
                c_data.Ref_redit_cart = tempo_credit_cart;
                tempo_credit_cart++;
                //Insert New Client

                my_connection.InsertClient(c_data);


            }
            int inv_cpt = 1;
            int nex_id = 1;
            foreach(XMLFilmData c_data in xml_film_data)
            {
                //Add film 
                my_connection.InsertFilm(c_data);
                c_data.bd_id = nex_id;
                nex_id++;
                //Add Scenariste
                foreach(string c_sceneriste in c_data.scenariste_list)
                {
                    my_connection.InsertSceneriste(c_data.bd_id, c_sceneriste);
                }
                //Add role
                foreach (XMLRoleData c_role in c_data.roles_list)
                {
                    my_connection.InsertRole(c_data.bd_id,c_role);
                }
                //Add Realisateur

                int ref_reali_id = SeachKey(dic_id_relation, c_data.realisateur_xml_id, xml_personne_data.Count);
                if(ref_reali_id <= 0)
                {
                    Console.WriteLine(c_data.xml_id);
                }
                else
                {
                    my_connection.InsertRealisateur(ref_reali_id, c_data.bd_id);

                }

               
                //Add Inventaire
                for (int i = 0;i< (int)(my_ramdom.NextDouble() * 99); i++)
                {
                    my_connection.InsertInvetaireCopy(c_data.bd_id);
                }
              


            }

            end_time_stamp = DateTime.Now.Millisecond;
            long insering_time = end_time_stamp - start_time_stamp;

            System.Console.WriteLine("XML Read count : Time ( " + xml_load_time + "  Millisecond )");
            System.Console.WriteLine("Data pre-process : Time ( " + data_preprocess_time + "  Millisecond )");
            System.Console.WriteLine("Data insertion : Time ( " + insering_time + "  Millisecond )");

            System.Console.WriteLine("\tFilm : " + xml_film_data.Count());
            System.Console.WriteLine("\tPersonne : " + xml_personne_data.Count());
            System.Console.WriteLine("\tClient : " + xml_clients_data.Count());
            System.Console.ReadKey();


            //Insert personne

        }
    }
}
