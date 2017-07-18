using ClientWeb.Models;
using System.Collections.Generic;

namespace ClientWeb.ViewModel
{
    public class FilmViewModel
    {
        public Client Client { get; set; }
        public Film Film { get; set; }
        public string Message { get; set; }
        public float Cote { get; set; }
        public List<Film> Recommandation { get; set; }
    }
}