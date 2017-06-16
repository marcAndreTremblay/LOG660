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
        public void Hibernate_Get_All_Client()
        {
            IList<Client> results = sut_session.QueryOver<Client>().List<Client>();        
            Assert.IsNotNull(results);
        }
    }
}
