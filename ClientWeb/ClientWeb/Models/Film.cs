using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientWeb.Models
{
    public class Film
    {
        public virtual List<Personne> Acteurs { get; set; }
        public virtual int AnneeDeSortie { get; set; }
        public virtual int DureeMinutes { get; set; }
        public virtual string Genres { get; set; }
        public virtual int Id { get; set; }
        public virtual string LangueOriginale { get; set; }

        [Display(Name = "Nombre de copies restantes")]
        public virtual int NbCopieRestante { get; set; }

        public virtual string Pays { get; set; }

        [Display(Name = "Réalisateur")]
        public virtual Realisateur Realisateur { get; set; }
        public virtual string Resume { get; set; }
        public virtual List<Scenariste> Scenaristes { get; set; }
        public virtual string Titre { get; set; }
    }
}