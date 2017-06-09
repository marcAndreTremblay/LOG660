namespace ClientWeb.Models
{
    public class FilmActeur
    {
        public Film Film { get; set; }
        public string Personnage { get; set; }
        public Personne Personne { get; set; }
    }
}