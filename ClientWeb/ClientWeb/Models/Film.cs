using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages;
using NHibernate;
using NHibernate.Criterion;

namespace ClientWeb.Models
{
    public class Film
    {
        public virtual ICollection<FilmActeur> FilmActeurs { get; set; }

        [Display(Name = "Année de sortie")]
        public virtual int AnneeSortie { get; set; }

        [Display(Name = "Durée du film")]
        public virtual int DureeMinutes { get; set; }

        public virtual string Genres { get; set; }
        public virtual int Id { get; set; }

        [Display(Name = "Langue originale")]
        public virtual string LangueOriginale { get; set; }

        [Display(Name = "Nombre de copies restantes")]
        public virtual int NbCopieRestante { get; set; }

        public virtual string Pays { get; set; }

        [Display(Name = "Réalisateur")]
        public virtual ICollection<Realisateur> Realisateurs { get; set; }

        [Display(Name = "Résumé")]
        public virtual string Resume { get; set; }

        //public virtual ICollection<Scenariste> Scenaristes { get; set; }
        public virtual string Titre { get; set; }

        public static IEnumerable<Film> RechercherFilmsParCriteres(string titre, string realisateur, string pays, string langueOriginale, string genre, string anneeSortie, string acteur, int limit, int offset)
        {
            throw new NotImplementedException();
        }
    }
}