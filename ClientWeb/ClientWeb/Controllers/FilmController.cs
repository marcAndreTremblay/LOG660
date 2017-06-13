using ClientWeb.Models;
using ClientWeb.ViewModel;
using NHibernate;
using System.Collections.Generic;
using System.Web.Mvc;

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
                return View(film);
            }
        }

        // GET: /Film/ListeFilm
        public ActionResult ListeFilm(FilmActionViewModel vm)
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }
            Film film = new Film();
            film.Id = 146;
            vm.Films = new List<Film>();
            vm.Films.Add(film);

            //
            return View(vm);
        }

        // GET: /Film/Recherche
        public ActionResult Recherche()
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new FilmActionViewModel());
        }
    }
}