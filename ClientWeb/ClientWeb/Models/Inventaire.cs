namespace ClientWeb.Models
{
    public class Inventaire
    {
        public virtual Film Film { get; set; }
        public virtual Inventaire Id { get; set; }
    }
}