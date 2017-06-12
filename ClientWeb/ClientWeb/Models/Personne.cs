using System;

namespace ClientWeb.Models
{
    public class Personne
    {
        public virtual string Biographie { get; set; }
        public virtual DateTime DateNaissance { get; set; }
        public virtual int Id { get; set; }
        public virtual string LieuNaissance { get; set; }
        public virtual string NomFamille { get; set; }
        public virtual string Prenom { get; set; }

    }
}