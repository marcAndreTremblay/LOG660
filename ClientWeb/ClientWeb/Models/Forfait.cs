namespace ClientWeb.Models
{
    public class Forfait
    {
        public int Id { get; set; }
        public int CoutParMois { get; set; }
        public string TypeForfait { get; set; }
        public int LocationMax { get; set; }
        public int DureeMaxJour { get; set; }
    }
}