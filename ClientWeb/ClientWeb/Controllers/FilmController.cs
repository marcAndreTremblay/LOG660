using ClientWeb.Models;
using ClientWeb.ViewModel;
using NHibernate;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;

namespace ClientWeb.Controllers
{
    public class FilmController : Controller
    {
        // GET: /Film/DetailsFilm
        public ActionResult DetailsFilm(int id)
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }
            using (ISession session = NHibernateSession.OpenSession())// get le film avec cet id j'ai essayé mais il manque quelque chose
            {
                var film = session.Get<Film>(id);
                FilmViewModel vm = new FilmViewModel();
                vm.film = film;
                return View(vm);
            }
        }

        // GET: /Film/ListeFilm
        public ActionResult ListeFilm(string titre, string realisateur, string pays, string langueOriginale, string genre, string anneeDeSortie, string acteur, int limit = 10, int offset = 0)
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }

            Film.RechercherFilmsParCriteres(titre, realisateur, pays, langueOriginale, genre, anneeDeSortie, acteur,
                limit, offset);
            ViewBag.Films = new List<Film>();
            ViewBag.Films.AddRange(Film.RechercherFilmsParCriteres(titre, realisateur, pays, langueOriginale, genre, anneeDeSortie, acteur,
                limit, offset));

            return View();
        }

        // GET: /Film/Recherche
        public ActionResult Recherche(string keyword, int limit, int offset)
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }


            return View(new FilmActionViewModel());
        }
    }
}