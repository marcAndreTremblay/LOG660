namespace ClientWeb.Models
{
    public class Scenariste
    {
        public virtual int Id { get; set; }
        public virtual Film Film { get; set; }
        public virtual string Nom { get; set; }
    }
}