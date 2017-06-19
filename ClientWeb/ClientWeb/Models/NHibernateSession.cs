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
            var scenaristeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Scenariste.hbm.xml");
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
            configuration.AddFile(scenaristeConfigurationFile);

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
        public static ISession OpenSessionNoServer()
        {
            var configuration = new Configuration();
            configuration.Configure(@".\ClientWeb\Models\Nhibernate\hibernate.cfg.xml");


                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\CarteCredit.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Client.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Employe.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Personne.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Film.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Forfait.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\FilmActeur.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Realisateur.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\LocationClient.hbm.xml");
                configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Inventaire.hbm.xml");
              configuration.AddFile(@".\ClientWeb\Models\Nhibernate\Scenariste.hbm.xml");

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}