using NHibernate;
using NHibernate.Cfg;
using System.Web;

namespace ClientWeb.Models
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var carteCreditConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\CarteCredit.hbm.xml");
            var clientConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Client.hbm.xml");
            var employeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Employe.hbm.xml");
            var personneConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Personne.hbm.xml");
            configuration.AddFile(carteCreditConfigurationFile);
            configuration.AddFile(clientConfigurationFile);
            configuration.AddFile(employeConfigurationFile);
            configuration.AddFile(personneConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}