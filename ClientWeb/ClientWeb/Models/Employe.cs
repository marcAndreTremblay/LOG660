namespace ClientWeb.Models
{
    public class Employe
    {
        public int Id { get; set; }
        public Personne Personne { get; set; }
        public Adresse Adresse { get; set; }
        public string NumeroTel { get; set; }
        public string Courriel { get; set; }
        public string Password { get; set; }
        public string Matricule { get; set; }
    }
}