using System.ComponentModel.DataAnnotations;

namespace ClientWeb.ViewModel
{
    public class ConnexionViewModel
    {
        public string Erreur { get; set; }

        [Display(Name = "Courriel ou matricule")]
        public string EmailOuMatricule { get; set; }

        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        public ConnexionViewModel()
        {
            Erreur = "";
        }
    }
}