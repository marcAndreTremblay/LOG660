using System.Collections.Generic;

namespace ClientWeb.Models
{
    public class Film
    {
        public string Titre { get; set; }
        public int AnneeDeSortie { get; set; }
        public List<string> Pays { get; set; }
        public string LangueOriginale { get; set; }
        public int DureeMinute { get; set; }
        public List<string> Genres { get; set; }
        public string NomRealisateur { get; set; }
        public string Senariste { get; set; }
        public List<Acteur> Acteurs { get; set; }
        public string Resume { get; set; }
    }
}