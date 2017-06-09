using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientWeb.Models
{
    public class Film
    {
        public List<Personne> Acteurs { get; set; }
        public int AnneeDeSortie { get; set; }
        public int DureeMinutes { get; set; }
        public string Genres { get; set; }
        public int Id { get; set; }
        public string LangueOriginale { get; set; }

        [Display(Name = "Nombre de copies restantes")]
        public int NbCopieRestante { get; set; }

        public string Pays { get; set; }
        public List<Realisateur> Realisateurs { get; set; }
        public string Resume { get; set; }
        public Scenariste Scenariste { get; set; }
        public string Titre { get; set; }
    }
}