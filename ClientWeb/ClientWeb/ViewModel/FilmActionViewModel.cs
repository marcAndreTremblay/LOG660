using ClientWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientWeb.ViewModel
{
    public class FilmActionViewModel
    {
        public int NbTotalPages { get; set; }
        public int NoPageActuelle { get; set; }
        public List<Film> Films { get; set; }

        public string Titre { get; set; }

        [Display(Name = "Annee de sortie")]
        public string AnneeSortie { get; set; }

        public string Pays { get; set; }

        [Display(Name = "Langue Originale")]
        public string LangueOriginale { get; set; }

        public string Genre { get; set; }
        public string Realisateur { get; set; }
        public string Acteur { get; set; }
        public string DureeMinutes { get; set; }
    }
}