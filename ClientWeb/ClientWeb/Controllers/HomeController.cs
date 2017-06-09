using ClientWeb.Models;
using ClientWeb.ViewModel;
using NHibernate;
using System.Web.Mvc;

namespace ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }
            using (ISession session = NHibernateSession.OpenSession())
            {
                int id = 607;
                var creditCard = session.Get<CarteCredit>(id);

                return View(creditCard);
            }
        }

        public ActionResult Connexion(ConnexionViewModel vm)
        {
            vm.AfficherErreur = false;
            if (Employe.SeConnecter(vm.Email, vm.MotDePasse) || Client.SeConnecter(vm.Email, vm.MotDePasse))
            {
                System.Web.HttpContext.Current.Session["EstConnecté"] = true;
                return RedirectToAction("Index");
            }
            if (vm.Email != null)
                vm.AfficherErreur = true;
            return View(vm);
        }

        public ActionResult Index()
        {
            GestionConnexion.estConnecte();
            return View();
        }
    }
}