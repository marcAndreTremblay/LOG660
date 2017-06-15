using System;

namespace ClientWeb.Models
{
    public class Personne
    {
        private string biographie;

        public virtual string Biographie
        {
            get
            {
                if (biographie == null)
                    biographie = "Aucune biographie";
                return biographie;
            }
            set
            {
                if (value == null)
                    biographie = "Aucune biographie";
                biographie = value;
            }
        }

        public virtual DateTime DateNaissance { get; set; }
        public virtual int Id { get; set; }
        public virtual string LieuNaissance { get; set; }
        public virtual string NomFamille { get; set; }
        public virtual string Prenom { get; set; }

        public Personne()
        {
            biographie = "Aucune biographie";
        }
    }
}