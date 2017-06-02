using ClientWeb.Models;
using System.Web.Mvc;

namespace ClientWeb.Controllers
{
    public class FilmController : Controller
    {
        // GET: /Film/Recherche
        public ActionResult Recherche()
        {
            return View();
        }

        // GET: /Film/DetailsFilm
        public ActionResult DetailsFilm(int index)
        {

            return View(new Film());
        }

        // GET: /Film/ListeFilm
        public ActionResult ListeFilm()
        {
            return View();
        }
    }
}