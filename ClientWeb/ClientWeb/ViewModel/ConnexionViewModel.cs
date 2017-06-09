using System.ComponentModel.DataAnnotations;

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