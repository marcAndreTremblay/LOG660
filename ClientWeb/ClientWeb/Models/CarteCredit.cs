namespace ClientWeb.Models
{
    public class CarteCredit
    {
        public int iD { get; set; }
        public string TypeCarte { get; set; }
        public string Numero { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public int Cvv { get; set; }
    }
}