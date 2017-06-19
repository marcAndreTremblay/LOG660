using System;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace ClientWeb.Models
{
    public class Client
    {
        public virtual Adresse Adresse { get; set; }
        public virtual CarteCredit CarteCredit { get; set; }
        public virtual string Courriel { get; set; }
        public virtual Forfait Forfait { get; set; }
        public virtual int Id { get; set; }
        public virtual List<LocationClient> Locations { get; set; }
        public virtual string NumeroTel { get; set; }
        public virtual string Password { get; set; }
        public virtual Personne Personne { get; set; }
        public int NbLocationsEnCours { get; set; }
    }
}