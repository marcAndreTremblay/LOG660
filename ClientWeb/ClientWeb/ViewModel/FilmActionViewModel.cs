using ClientWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientWeb.ViewModel
{
    public class FilmActionViewModel
    {
        public List<Film> Films { get; set; }

        [Display(Name = "Rechercher")]
        [Required]
        public string LaRecherche { get; set; }
    }
}