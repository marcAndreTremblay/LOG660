namespace ClientWeb.Models
{
    public class FilmActeur
    {
        public Personne Personne { get; set; }
        public Film Film { get; set; }
        public string Personnage { get; set; }
    }
}