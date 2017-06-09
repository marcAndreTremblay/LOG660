using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientWeb.ViewModel
{
    public class ConnexionViewModel
    {
        public bool AfficherErreur { get; set; }
        public string Email { get; set; }

        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }
    }
}