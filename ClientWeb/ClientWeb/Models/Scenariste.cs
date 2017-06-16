namespace ClientWeb.Models
{
    public class Scenariste
    {
        public virtual int Id { get; set; }
        public virtual Film Film { get; set; }
        public virtual string Nom { get; set; }
        /*public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode = hashCode ^ Film.GetHashCode() ^ Nom.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var toCompare = obj as Scenariste;
            if (toCompare == null)
            {
                return false;
            }
            return (this.GetHashCode() != toCompare.GetHashCode());
        }*/
    }
}