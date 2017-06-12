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

        [HttpPost]
        public ActionResult Connexion(ConnexionViewModel vm)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                if (vm.EmailOuMatricule == null || vm.MotDePasse == null)
                {
                    vm.Erreur = "Veuillez entrer tous les informations demandées";
                    return View(vm);
                }

                Client Client = Client.TrouverClientParCourrielEtMotDePasse(vm.EmailOuMatricule, vm.MotDePasse);
                Employe Employe = Employe.TrouverEmployeParMatriculeEtMotDePasse(vm.EmailOuMatricule, vm.MotDePasse);

                if (Client != null)
                {
                    System.Web.HttpContext.Current.Session["UtilisateurConnecté"] = Client;
                    return RedirectToAction("Index");
                }
                else if (Employe != null)
                {
                    System.Web.HttpContext.Current.Session["UtilisateurConnecté"] = Employe;
                    return RedirectToAction("Index");
                }
                else
                {
                    vm.Erreur = "Courriel/Matricule ou mot de passe invalide. Veuillez réessayer";
                }

                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult Connexion()
        {
            return View(new ConnexionViewModel());
        }

        public ActionResult Deconnexion(ConnexionViewModel vm)
        {
            System.Web.HttpContext.Current.Session["UtilisateurConnecté"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            GestionConnexion.estConnecte();
            return View();
        }
    }
}