using System;
using ClientWeb.Models;
using ClientWeb.ViewModel;
using NHibernate;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;
using ClientWeb.DAO.Nhibernate;

namespace ClientWeb.Controllers
{
    public class FilmController : Controller
    {
        // GET: /Film/DetailsFilm
        public ActionResult DetailsFilm(int id)
        {
            List<Film> recommendations = new List<Film>();
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }

            FilmDao filmDao = new FilmDao();
            LocationClientDao locationClientDao = new LocationClientDao();

            var film = filmDao.GetFilmParId(id);
            film.NbCopieRestante = filmDao.GetNbCopiesRestantes(id);

            int[] recommendationsIds = filmDao.GetRecommendationsForFilmId(id);

            recommendations.Add(filmDao.GetFilmParId(id[0]));
            recommendations.Add(filmDao.GetFilmParId(id[1]));
            recommendations.Add(filmDao.GetFilmParId(id[2]));

            int nbRented =
                locationClientDao.GetNumberOfRentedCopiesByClientIdAndFilmId(
                    ((Client)System.Web.HttpContext.Current.Session["UtilisateurConnecté"]).Id, id);

            Client client = (Client)System.Web.HttpContext.Current.Session["UtilisateurConnecté"];
            client.NbLocationsEnCours = locationClientDao.GetNbLocationsEnCoursByClientId(client.Id);

            FilmViewModel vm = new FilmViewModel
            {
                Film = film,
                Client = (Client)System.Web.HttpContext.Current.Session["UtilisateurConnecté"],
                Message = nbRented > 0 ? "Vous avez présentement " + nbRented + " copie(s) de ce film de loué" : "",
                Recommandation = recommendations,
                Cote = filmDao.GetCoteMoyenneForFilmId(id)
            };
            return View(vm);
        }

        // GET/POST: /Film/ListeFilm
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

            FilmDao filmDao = new FilmDao();

            if (titre == null && realisateur == null && pays == null && langueOriginale == null && genre == null && anneeSortie == null && acteur == null)
            {
                vm.PremiereFois = true;
            }
            else if (!(titre == "" && realisateur == "" && pays == "" && langueOriginale == "" && genre == "" && anneeSortie == "" && acteur == ""))
            {
                vm.Films.AddRange(filmDao.RechercherFilmsParCriteres(titre, realisateur, pays, langueOriginale, genre, anneeSortie, acteur,
                limit, offset));
                vm.NbTotalPages = (filmDao.CountFilmsCriteres(titre, realisateur, pays, langueOriginale, genre, anneeSortie, acteur) + limit - 1) / limit; ;
            }

            return View(vm);
        }

        public ActionResult LouerCopie(int id)
        {
            if (!GestionConnexion.estConnecte())
            {
                return RedirectToAction("Index", "Home");
            }

            FilmDao filmDao = new FilmDao();

            filmDao.LouerCopie(id, ((Client)System.Web.HttpContext.Current.Session["UtilisateurConnecté"]).Id);

            return RedirectToAction("DetailsFilm", "Film", new { id });
        }
    }
}