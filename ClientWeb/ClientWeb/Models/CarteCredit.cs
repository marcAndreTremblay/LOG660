namespace ClientWeb.Models
{
    public class CarteCredit
    {
        public virtual int Id { get; set; }
        public virtual string TypeCarte { get; set; }
        public virtual string Numero { get; set; }
        public virtual int ExpMonth { get; set; }
        public virtual int ExpYear { get; set; }
        public virtual int Cvv { get; set; }
    }
}