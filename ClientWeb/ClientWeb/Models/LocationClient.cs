using System;

namespace ClientWeb.Models
{
    public class LocationClient
    {
        public virtual Client Client { get; set; }
        public virtual DateTime DateLocation { get; set; }
        public virtual DateTime DateRetour { get; set; }
        public virtual int Id { get; set; }
        public virtual Inventaire Inventaire { get; set; }
    }
}