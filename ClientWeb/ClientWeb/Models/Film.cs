using System.Collections.Generic;

namespace ClientWeb.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int AnneeDeSortie { get; set; }
        public List<string> Pays { get; set; }
        public string LangueOriginale { get; set; }
        public int DureeMinutes { get; set; }
        public List<string> Genres { get; set; }
        public List<Realisateur> Realisateurs { get; set; }
        public Scenariste Scenariste { get; set; }
        public List<Acteur> Acteurs { get; set; }
        public string Resume { get; set; }
    }
}