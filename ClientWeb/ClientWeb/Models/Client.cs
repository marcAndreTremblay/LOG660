namespace ClientWeb.Models
{
    public class Client
    {
        public int Id { get; set; }
        public Personne Personne { get; set; }
        public Forfait Forfait { get; set; }
        public Adresse Adresse { get; set; }
        public CarteCredit CarteCredit { get; set; }
        public string NumeroTel { get; set; }
        public string Courriel { get; set; }
        public string Password { get; set; }
    }
}