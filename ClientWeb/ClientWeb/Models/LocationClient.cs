using System;

namespace ClientWeb.Models
{
    public class LocationClient
    {
        public int Id { get; set; }
        public Inventaire Inventaire { get; set; }
        public Client Client { get; set; }
        public DateTime DateLocation { get; set; }
        public DateTime DateRetour { get; set; }
    }
}