using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.IO;
using System.Text;

namespace LOG660_InsertionProgram
{



    public class XMLRoleData {

        //Insert this into acteur table
        public int acteur_id;
        public string acteur_name;

        string personnage_name;
        public XMLRoleData()
        {

        }
    };
    public class XMLFilmData
    {
<<<<<<< HEAD
        public int id;
        public string title;
        public int year;
        public string pays;
        public string langue;
        public int duréé; //Minute
        public string resumer;

        public List<string> genre_list;
        public List<XMLRoleData> roles_list;
        public List<string> annonce_list;
        public List<string> scenariste_list;
=======
        int id;
        string title;
        int year;
        string pays;
        string langue;
        int duree; //Minute
        string resumer;

        List<string> genre_list;
        List<XMLRoleData> roles_list;
        List<string> annonce_list;
        List<string> scenariste_list;
>>>>>>> 010b6ee6d069c8f191ba186eea1a5ef68bfba8dd

        //For table realisateur
        public string realisateur_id;
        public string realisateur_name;

        public XMLFilmData()
        {
            genre_list = new List<string>();
            roles_list = new List<XMLRoleData>();
            scenariste_list = new List<string>();
            annonce_list = new List<string>();
        }
    }


    public class XMLInfoCredit
    {
        public string carte_type; //Mastercarte - Visa - American ezpress
        public string No;
        public int exp_mount;
        public int exp_year;

        public XMLInfoCredit()
        {

        }
    }
    public class XMLClientData
    {
<<<<<<< HEAD
        public int bd_id;
        public int xml_id;
        public string last_name;
        public string first_name;
        public string courriel;
        public string telephone;
        public string aniversaire;
        public string address;
        public string ville;
        public string province;
        public string code_postal;
        public XMLInfoCredit credit_carte_indo;
        public string mot_de_passe;
        public string forfait; //char(1)
=======
        int id;
        string last_name;
        string first_name;
        string courriel;
        string telephone;
        string aniversaire;
        string address;
        string ville;
        string province;
        string code_postal;
        XMLInfoCredit credit_carte_info;
        string mot_de_passe;
        string forfait; //char(1)
>>>>>>> 010b6ee6d069c8f191ba186eea1a5ef68bfba8dd

        public XMLClientData()
        {

        }

    }

    public class XMLNaissanceData
    {
        public string data; //yyyy-MM-JJ
        public string lieu; // city, state, country
        public XMLNaissanceData()
        {

        }
    }
    public class XMLPersonneData
    {
        public int db_id;
        public int xml_id;
        public string name;
        public string biographie;
        public XMLNaissanceData naissance;
        public string photo_link;

        public XMLPersonneData()
        {

        }
    }
}
