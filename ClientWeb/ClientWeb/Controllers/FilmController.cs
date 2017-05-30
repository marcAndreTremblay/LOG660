using ClientWeb.Models;
using System.Web.Mvc;

namespace ClientWeb.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        public ActionResult Recherche()
        {
            return View();
        }
        public ActionResult DetailsFilm(int index)
        {

            return View(new Film());
        }
        public ActionResult ListeFilm()
        {
            return View();
        }
    }
}