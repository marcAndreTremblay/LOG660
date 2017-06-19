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

         ISession sut_session;
        [SetUp]
        public void Init()
        {
            session = ClientSession.GetClientSession();

            sut_session = NHibernateSession.OpenSessionNoServer();
        }

        [Test]
        public void HibernateSessionTest()
        {
             Assert.IsNotNull(sut_session);
        }

            

        [Test]
        public void Hibernate_Integrity_Test_Client()
        {
            Client results = sut_session.Get<Client>(1);
            Assert.IsNotNull(results);
        }
        [Test]
        public void Hibernate_Integrity_Test_Film()
        { 
            IList<Film> results = sut_session.QueryOver<Film>().List<Film>();

            Assert.IsNotNull(results);
        }
        [Test]
        public void Hibernate_Integrity_Test_Film_Acteur()
        {
            IList<FilmActeur> results = sut_session.QueryOver<FilmActeur>().List<FilmActeur>();
            Assert.IsNotNull(results);
        }



        [TestCase("root", "root", true, TestName = "right password , rignt matricule")]
        [TestCase("sadads", "root", false , TestName = "Wrong password , rignt matricule")]
        [TestCase("dsdadsd", "dsadads",false, TestName = "Wrong password , wrong matricule")]
        [TestCase("root", "dsadada",false, TestName = "right password , wrong matricule")]
        public void Employe_Connection(string mdp, string matricule,bool expected_value)
        {
            Employe sut = Employe.TrouverEmployeParMatriculeEtMotDePasse(matricule, mdp);
            bool result;
            if(sut == null) {
                result = false;
            }
            else {
                   result =  true;
            }
            Assert.AreEqual(result, expected_value);
        }
        [TestCase("hello123", "AnneDBrown75@hotmail.com", true, TestName = "right password , rignt username")]
        [TestCase("sadads", "AnneDBrown75@hotmail.com", false, TestName = "Wrong password , rignt username")]
        [TestCase("hello123", "dsadads", false, TestName = "Wrong password , wrong username")]
        [TestCase("root", "dsadada", false, TestName = "right password , wrong username")]
        public void Client_Connection(string mdp, string email, bool expected_value)
        {
            ClientDao sut =new  ClientDao();
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
    }
    
}
