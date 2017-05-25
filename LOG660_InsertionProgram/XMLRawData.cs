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
        int acteur_id;
        string acteur_name;

        string personnage_name;
        public XMLRoleData()
        {

        }
    };
    public class XMLFilmData
    {
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

        //For table realisateur
        string realisateur_id;
        string realisateur_name;

        public XMLFilmData()
        {
            genre_list = new List<string>();
            roles_list = new List<XMLRoleData>();
        }
    }


    public class XMLInfoCredit
    {
        string carte_type; //Mastercarte - Visa - American ezpress
        string No;
        int exp_mount;
        int exp_year;

        public XMLInfoCredit()
        {

        }
    }
    public class XMLClientData
    {
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

        public XMLClientData()
        {

        }

    }

    public class XMLNaissanceData
    {
        string data; //yyyy-MM-JJ
        string lieu; // city, state, country
        public XMLNaissanceData()
        {

        }
    }
    public class XMLPersonneData
    {
        int id;
        string name;
        string biographie;
        XMLNaissanceData naissance;
        string photo_link;

        public XMLPersonneData()
        {

        }
    }
}
