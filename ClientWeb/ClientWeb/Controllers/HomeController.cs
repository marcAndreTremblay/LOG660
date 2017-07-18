using ClientWeb.Models;
using ClientWeb.ViewModel;
using NHibernate;
using System.Web.Mvc;
using ClientWeb.DAO.Nhibernate;

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
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                int id = 607;
                var creditCard = session.Get<CarteCredit>(id);

                return View(creditCard);
            }
        }

        [HttpPost]
        public ActionResult Connexion(ConnexionViewModel vm)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                if (vm.EmailOuMatricule == null || vm.MotDePasse == null)
                {
                    vm.Erreur = "Veuillez entrer tous les informations demandées";
                    return View(vm);
                }

                ClientDao clientDao = new ClientDao();
                EmployeDao employeDao = new EmployeDao();

                Client client = clientDao.GetClientParCourrielEtMotDePasse(vm.EmailOuMatricule, vm.MotDePasse);
                Employe employee = employeDao.GetEmployeParMatriculeEtMotDePasse(vm.EmailOuMatricule, vm.MotDePasse);

                if (client != null)
                {
                    System.Web.HttpContext.Current.Session["UtilisateurConnecté"] = client;
                    return RedirectToAction("Index");
                }
                else if (employee != null)
                {
                    System.Web.HttpContext.Current.Session["UtilisateurConnecté"] = employee;
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