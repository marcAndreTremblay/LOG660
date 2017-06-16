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
        [Test]
        public void HibernateSessionTest()
         {
             ISession session = NHibernateSession.OpenSessionNoServer();
             Assert.IsNotNull(session);
         }
    }
}
