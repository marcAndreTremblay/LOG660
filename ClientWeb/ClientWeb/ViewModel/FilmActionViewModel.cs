using ClientWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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