using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using NHibernate;
using ClientWeb.Models;



namespace ClientWeb.Tests
{
    [TestFixture]
    public class HibernateTest
      {
        ISession sut_session;
        [SetUp]
        public void Init()
        {
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
        
    }
}
