using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace LOG660_InsertionProgram
{

    public class ForfaitData
    {
        public int id_bd;
        public char Nom;

        public ForfaitData()
        {

        }
    }

        public class XMLRoleData {

        //Insert this into acteur table
        public int xml_personne_id;  //Ref to a personne
        public string personnage_name;

        //Tempo gen data
        public int ref_personne_bd_id;

        public XMLRoleData()
        {

        }
    };
    public class XMLFilmData
    {
        public int xml_id;
        public int bd_id;
        public string title;
        public int year;
        public string pays;
        public string langue;
        public int duree; //Minute
        public string resumer;

        public List<string> genre_list;
        public List<XMLRoleData> roles_list;
        public List<string> annonce_list;
        public List<string> scenariste_list;


        //For table realisateur
        public int realisateur_xml_id;

        public XMLFilmData()
        {
            genre_list = new List<string>();
            roles_list = new List<XMLRoleData>();
            scenariste_list = new List<string>();
            annonce_list = new List<string>();
        }
        static public FilmComparer GetComparer()
        {
            return new FilmComparer();
        }
    }
    public class FilmComparer : IComparer<XMLFilmData>
    {
        public FilmComparer()
        {

        }
        public int Compare(XMLFilmData x, XMLFilmData y)
        {
            if (x.xml_id == y.xml_id)
            {
                return 0;
            }
            if (x.xml_id > y.xml_id)
            {
                return 1;
            }
            return -1;
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

        //Generated data on runtime
        public int Ref_Forfait_id;

        public XMLClientData()
        {
            credit_carte_indo = new XMLInfoCredit();
        }
        static public ClientComparer GetComparer()
        {
            return new ClientComparer();
        }

    }
    public class ClientComparer : IComparer<XMLClientData>
    {
        public ClientComparer()
        {

        }
        public int Compare(XMLClientData x, XMLClientData y)
        {
            if (x.xml_id == y.xml_id)
            {
                return 0;
            }
            if (x.xml_id > y.xml_id)
            {
                return 1;
            }
            return -1;
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
        public string last_name;
        public string biographie;
        public XMLNaissanceData naissance_info;
        public string photo_link;

        public XMLPersonneData()
        {
            naissance_info = new XMLNaissanceData();
        }
        static public PersonneComparer GetComparer()
        {
            return new PersonneComparer();
        }

    }
    public class PersonneComparer : IComparer<XMLPersonneData>
    {
        public PersonneComparer()
        {

        }
        public int Compare(XMLPersonneData x, XMLPersonneData y)
    {
        if (x.xml_id == y.xml_id)
        {
            return 0;
        }
        if (x.xml_id > y.xml_id)
        {
            return 1;
        }
        return -1;
    }
    }
}
