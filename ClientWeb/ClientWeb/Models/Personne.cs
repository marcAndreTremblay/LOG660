using System;

namespace ClientWeb.Models
{
    public class Personne
    {
        public int Id { get; set; }
        public string Prenom { get; set; }
        public string NomFamille { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Biographie { get; set; }
        public string LieuNaissance { get; set; }

    }
}