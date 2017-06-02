using ClientWeb.Models;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Web.Mvc;

namespace ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                int id = 607;
                var creditCard = session.Get<CarteCredit>(id);

                return View(creditCard);
            }
            
        }

        public ActionResult Connexion()
        {
            return View();
        }
    }
}