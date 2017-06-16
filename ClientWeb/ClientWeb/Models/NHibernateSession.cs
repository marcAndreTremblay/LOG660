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
            var filmConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Film.hbm.xml");
            var forfaitConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Forfait.hbm.xml");
            var filmActeurConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\FilmActeur.hbm.xml");
            var realisateurConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Realisateur.hbm.xml");
            var locationClientConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\LocationClient.hbm.xml");
            var inventaireConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Inventaire.hbm.xml");
            configuration.AddFile(carteCreditConfigurationFile);
            configuration.AddFile(clientConfigurationFile);
            configuration.AddFile(employeConfigurationFile);
            configuration.AddFile(personneConfigurationFile);
            configuration.AddFile(filmConfigurationFile);
            configuration.AddFile(forfaitConfigurationFile);
            configuration.AddFile(filmActeurConfigurationFile);
            configuration.AddFile(realisateurConfigurationFile);
            configuration.AddFile(locationClientConfigurationFile);
            configuration.AddFile(inventaireConfigurationFile);

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
        public static ISession OpenSessionNoServer()
        {
            var configuration = new Configuration();
            var configurationPath = @".\ClientWeb\Models\Nhibernate\hibernate.cfg.xml";

            configuration.Configure(configurationPath);

            var carteCreditConfigurationFile = @".\ClientWeb\Models\Nhibernate\CarteCredit.hbm.xml";
            var clientConfigurationFile = @".\ClientWeb\Models\Nhibernate\Client.hbm.xml";
            var employeConfigurationFile = @".\ClientWeb\Models\Nhibernate\Employe.hbm.xml";
            var personneConfigurationFile = @".\ClientWeb\Models\Nhibernate\Personne.hbm.xml";
            var filmConfigurationFile =  @".\ClientWeb\Models\Nhibernate\Film.hbm.xml";
            var forfaitConfigurationFile = @".\ClientWeb\Models\Nhibernate\Forfait.hbm.xml";
            var filmActeurConfigurationFile = @".\ClientWeb\Models\Nhibernate\FilmActeur.hbm.xml";
            var realisateurConfigurationFile =@".\ClientWeb\Models\Nhibernate\Realisateur.hbm.xml";
            var locationClientConfigurationFile =@".\ClientWeb\Models\Nhibernate\LocationClient.hbm.xml";

            configuration.AddFile(carteCreditConfigurationFile);
            configuration.AddFile(clientConfigurationFile);
            configuration.AddFile(employeConfigurationFile);
            configuration.AddFile(personneConfigurationFile);


              configuration.AddFile(filmConfigurationFile);
            configuration.AddFile(forfaitConfigurationFile);
            configuration.AddFile(filmActeurConfigurationFile);
            configuration.AddFile(realisateurConfigurationFile);
            configuration.AddFile(locationClientConfigurationFile);
            configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Inventaire.hbm.xml");

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}