using ClientWeb.Models;
using ClientWeb.ViewModel;
using System.Web.Mvc;

namespace ClientWeb.Controllers
{
    public class FilmController : Controller
    {
        // GET: /Film/Recherche
        public ActionResult Recherche()
        {
            return View(new FilmActionViewModel());
        }

        // GET: /Film/DetailsFilm
        public ActionResult DetailsFilm(int id)
        {
            // requête pour get le film
            return View(new Film());
        }

        // GET: /Film/ListeFilm
        public ActionResult ListeFilm(FilmActionViewModel vm)
        {
            return View(vm);
        }
    }
}