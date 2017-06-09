namespace ClientWeb.Models
{
    public class Employe
    {
        public Adresse Adresse { get; set; }
        public string Courriel { get; set; }
        public int Id { get; set; }
        public string Matricule { get; set; }
        public string NumeroTel { get; set; }
        public string Password { get; set; }
        public Personne Personne { get; set; }

        public static bool SeConnecter(string email, string mdp)// a faire
        {
            return email == "test" && mdp == "test";
        }
    }
}