using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using System.Web;
using NHibernate;
using NHibernate.Cfg;
using ClientWeb.Models;

namespace ClientWeb.Tests.HibernateTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HibernateConnectionTest()
        {
            ISession session = NHibernateSession.OpenSessionNoServer();
            Assert.IsNotNull(session);
        }
    }
}
