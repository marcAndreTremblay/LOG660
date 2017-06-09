using System;

namespace ClientWeb.Models
{
    public class Personne
    {
        public string Biographie { get; set; }
        public DateTime DateNaissance { get; set; }
        public int Id { get; set; }
        public string LieuNaissance { get; set; }
        public string NomFamille { get; set; }
        public string Prenom { get; set; }
    }
}