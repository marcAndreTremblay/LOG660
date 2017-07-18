using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using NHibernate.Criterion;

using NHibernate;
using ClientWeb.Models;

using ClientWeb.DAO;
using ClientWeb.DAO.Nhibernate;

namespace ClientWeb.Tests
{
    [TestFixture]
    public class HibernateTest
      {
        ClientSession session;
        [SetUp]
        public void Init()
        {
            session = ClientSession.GetClientSession();
        }

        [Test]
        public void Hibernate_Integrity_Test_Session()
        {
             Assert.IsNotNull(session);
        }
        [Test]
        public void Hibernate_Integrity_Test_Client()
        {
            Client results = session.OpenSession().Get<Client>(1);
            Assert.IsNotNull(results);
        }
        [Test]
        public void Hibernate_Integrity_Test_Film()
        { 
            IList<Film> results = session.OpenSession().QueryOver<Film>().List<Film>();

            Assert.IsNotNull(results);
        }
        [Test]
        public void Hibernate_Integrity_Test_Film_Acteur()
        {
            IList<FilmActeur> results = session.OpenSession().QueryOver<FilmActeur>().List<FilmActeur>();
            Assert.IsNotNull(results);
        }



        [TestCase("root", "root", true, TestName = "Employe search : Right password , Rignt matricule")]
        [TestCase("sadads", "root", false , TestName = "Employe search : Wrong password , Rignt matricule")]
        [TestCase("dsdadsd", "dsadads",false, TestName = "Employe search : Wrong password , Wrong matricule")]
        [TestCase("root", "dsadada",false, TestName = "Employe search : Right password , Wrong matricule")]
        public void Employe_Connection(string mdp, string matricule,bool expected_value)
        {
            EmployeDao sut = new EmployeDao();
            Employe result_employe = sut.GetEmployeParMatriculeEtMotDePasse(matricule, mdp);
            bool result;
            if(result_employe == null) {
                result = false;
            }
            else {
                   result =  true;
            }
            Assert.AreEqual(result, expected_value);
        }
        [TestCase("hello123", "AnneDBrown75@hotmail.com", true, TestName = "Client connection : Right password , Rignt username")]
        [TestCase("sadads", "AnneDBrown75@hotmail.com", false, TestName =  "Client connection : Wrong password , Rignt username")]
        [TestCase("hello123", "dsadads", false, TestName =  "Client connection : Wrong password , Wrong username")]
        [TestCase("root", "dsadada", false, TestName = "Client connection : Right password , Wrong username")]
        public void Client_Connection(string mdp, string email, bool expected_value)
        {
            ClientDao sut = new  ClientDao();
            Client result_client = sut.GetClientParCourrielEtMotDePasse(email, mdp);
            bool result;
            if (result_client == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            Assert.AreEqual(result, expected_value);
        }



        [TestCase(1, true, TestName = "Film seach : Valid ID")]
        [TestCase(40000, false, TestName = "Film seach: Large invalid ID")]
        [TestCase(-100, false, TestName = "Film seach : Negative invalid ID")]
        public void Film_Seach_test(int target_id, bool expected_value)
        {
            FilmDao sut = new FilmDao();
            Film result_data = sut.GetFilmParId(target_id);
            bool result;
            if (result_data == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            Assert.AreEqual(result, expected_value);
        }
        [TestCase("", "", "", "", "", "", "", 631, TestName = "Film seach: No atribute")]
        [TestCase("Gran Torino", "", "", "", "", "", "", 1, TestName = "Film seach : Valid Name")]
        [TestCase("aaaaaaaaaaaaaa", "", "", "", "", "", "", 0, TestName = "Film seach : Invalid Name")]
        [TestCase("", "", "", "English", "", "", "", 587, TestName = "Film seach : Valid langue origine")]
        [TestCase("", "", "", "aaaaaaaaaaaaaa", "", "", "", 0, TestName = "Film seach: Invalid langue origine")]
        [TestCase("", "", "", "", "Comedy", "", "", 236, TestName = "Film seach : Valid Genre")]
        [TestCase("", "", "", "", "aaaaaaaaaaaaaa", "", "", 0, TestName = "Film seach : Invalid Genre")]
        public void Film_Seach_test(string titre, string realisateur, string pays, string langueOriginale,
            string genre, string anneeSortie, string acteur, int expedted)
        {
            FilmDao sut = new FilmDao();
            int cpt = sut.CountFilmsCriteres(titre, realisateur, pays, langueOriginale, genre, anneeSortie, acteur);
         
            Assert.AreEqual(expedted,cpt);
        }

        [Test]
        public void Film_Location_Transaction()
        {
            FilmDao film_dao = new FilmDao();
            ClientDao client_dao = new ClientDao();
            Client result_client = client_dao.GetClientParCourrielEtMotDePasse("AnneDBrown75@hotmail.com", "hello123");


            Film f1 = film_dao.GetFilmParId(1);
            int check1 = film_dao.GetNbCopiesRestantes(f1.Id);
            film_dao.LouerCopie(f1.Id, result_client.Id);
            int check2 = film_dao.GetNbCopiesRestantes(f1.Id);

            Assert.AreEqual(check1 - 1, check2);
        }
    }
    

}
