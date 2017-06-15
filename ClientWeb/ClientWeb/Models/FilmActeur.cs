using System.Collections.Generic;

namespace ClientWeb.Models
{
    public class FilmActeur
    {
        public virtual Personne Personne { get; set; }
        public virtual Film Film { get; set; }
        public virtual string Personnage { get; set; }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode = hashCode ^ Personne.GetHashCode() ^ Film.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var toCompare = obj as FilmActeur;
            if (toCompare == null)
            {
                return false;
            }
            return (this.GetHashCode() != toCompare.GetHashCode());
        }
    }
}