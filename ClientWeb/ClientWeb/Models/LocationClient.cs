using System;

namespace ClientWeb.Models
{
    public class LocationClient
    {
        public Client Client { get; set; }
        public DateTime DateLocation { get; set; }
        public DateTime DateRetour { get; set; }
        public int Id { get; set; }
        public Inventaire Inventaire { get; set; }
    }
}