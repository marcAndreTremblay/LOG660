namespace ClientWeb.Models
{
    public class Forfait
    {
        public virtual int CoutParMois { get; set; }
        public virtual int DureeMaxJour { get; set; }
        public virtual int Id { get; set; }
        public virtual int LocationMax { get; set; }
        public virtual string TypeForfait { get; set; }
    }
}