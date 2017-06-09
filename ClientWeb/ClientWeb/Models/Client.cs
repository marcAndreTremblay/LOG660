using System.Collections.Generic;

namespace ClientWeb.Models
{
    public class Client
    {
        public Adresse Adresse { get; set; }
        public CarteCredit CarteCredit { get; set; }
        public string Courriel { get; set; }
        public Forfait Forfait { get; set; }
        public int Id { get; set; }
        public List<LocationClient> locations { get; set; }
        public string NumeroTel { get; set; }
        public string Password { get; set; }
        public Personne Personne { get; set; }

        public static bool SeConnecter(string email, string mdp)// a faire
        {
            return email == "test" && mdp == "test";
        }

        public int GetNbLocationsEnCours()
        {
            int nb = 0;
            foreach (LocationClient l in locations)
            {
                if (l.DateRetour == null)
                    nb++;
            }
            return nb;
        }
    }
}