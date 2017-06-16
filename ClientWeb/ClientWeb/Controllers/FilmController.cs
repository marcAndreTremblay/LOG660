using System;
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

            var film = Film.ChercherFilmParId(id);
            var location = LocationClient.GetLocationByClientIdAndFilmId(((Client)System.Web.HttpContext.Current.Session["UtilisateurConnecté"]).Id, id);

            FilmViewModel vm = new FilmViewModel
            {
                Film = film,
                Client = (Client) System.Web.HttpContext.Current.Session["UtilisateurConnecté"],
                Message = location != null ? "Vous avez présentement une copie de ce film de loué" : ""
            };
            return View(vm);
        }

        // GET: /Film/ListeFilm
        public ActionResult ListeFilm(string titre, string realisateur, string pays, string langueOriginale, string genre, string anneeSortie, string acteur, int limit = 10, int page = 1)
        {
            int offset = (page - 1) * limit;
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }
            FilmActionViewModel vm = new FilmActionViewModel
            {
                NbTotalPages = 10,//mettre le vrai nombre ceiling(nb film/limit)
                NoPageActuelle = page,
                Films = new List<Film>()
            };
            
            vm.Films.AddRange(Film.RechercherFilmsParCriteres(titre, realisateur, pays, langueOriginale, genre, anneeSortie, acteur,
                limit, offset));

            return View(vm);
        }

        public ActionResult LouerCopie(int id)
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }

            Film.LouerCopie(id, ((Client) System.Web.HttpContext.Current.Session["UtilisateurConnecté"]).Id);

            return RedirectToAction("DetailsFilm", "Film", new { id });
        }
    }
}