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
            film.NbCopieRestante = Film.GetNbCopiesRestantes(id);
            int nbRented =
                LocationClient.GetNumberOfRentedCopiesByClientIdAndFilmId(
                    ((Client)System.Web.HttpContext.Current.Session["UtilisateurConnecté"]).Id, id);

            FilmViewModel vm = new FilmViewModel
            {
                Film = film,
                Client = (Client)System.Web.HttpContext.Current.Session["UtilisateurConnecté"],
                Message = nbRented > 0 ? "Vous avez présentement " + nbRented + " copie(s) de ce film de loué" : ""
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
                NbTotalPages = 0,
                NoPageActuelle = page,
                Films = new List<Film>(),
                PremiereFois = false
            };
            if (titre == null && realisateur == null && pays == null && langueOriginale == null && genre == null && anneeSortie == null && acteur == null)
            {
                vm.PremiereFois = true;
            }
            else if (!(titre == "" && realisateur == "" && pays == "" && langueOriginale == "" && genre == "" && anneeSortie == "" && acteur == ""))
            {
                vm.Films.AddRange(Film.RechercherFilmsParCriteres(titre, realisateur, pays, langueOriginale, genre, anneeSortie, acteur,
                limit, offset));
                vm.NbTotalPages = (Film.CountFilmsCriteres(titre, realisateur, pays, langueOriginale, genre, anneeSortie, acteur) + limit - 1) / limit; ;
            }

            return View(vm);
        }

        public ActionResult LouerCopie(int id)
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }

            Film.LouerCopie(id, ((Client)System.Web.HttpContext.Current.Session["UtilisateurConnecté"]).Id);

            return RedirectToAction("DetailsFilm", "Film", new { id });
        }
    }
}