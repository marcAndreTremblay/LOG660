namespace ClientWeb.Models
{
    public class Realisateur
    {
        public virtual Film Film { get; set; }
        public virtual Personne Personne { get; set; }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode = hashCode ^ Personne.GetHashCode() ^ Film.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var toCompare = obj as Realisateur;
            if (toCompare == null)
            {
                return false;
            }
            return (this.GetHashCode() != toCompare.GetHashCode());
        }
    }
}