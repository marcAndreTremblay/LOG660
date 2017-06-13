using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages;
using NHibernate;
using NHibernate.Criterion;

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

        public static IList<Film> RechercherFilmParKeyword(string keyword, int limit, int offset)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                IList<Film> films = null;

                using (var tx = session.BeginTransaction())
                {
                    films = session.CreateCriteria<Film>()
                        .Add(Restrictions.InsensitiveLike("Titre", keyword, MatchMode.Anywhere))
                        .SetMaxResults(limit)
                        .SetFirstResult(offset)
                        .List<Film>(); ;

                        tx.Commit();
                }

                return films;
            }
        }

        public static IList<Film> RechercherFilmsParCriteres(string titre, string realisateur, string pays, string langueOriginale, string genre, string anneeDeSortie, string acteur, int limit, int offset)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                IList<Film> films = null;

                using (var tx = session.BeginTransaction())
                {
                    ICriteria criteria = session.CreateCriteria<Film>();

                    if (!titre.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("Titre", titre, MatchMode.Anywhere));
                    }
                    if (!realisateur.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("Realisateur", realisateur, MatchMode.Anywhere));
                    }
                    if (!pays.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("Pays", pays, MatchMode.Anywhere));
                    }
                    if (!langueOriginale.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("LangueOriginale", langueOriginale, MatchMode.Anywhere));
                    }
                    if (!genre.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("Genre", genre, MatchMode.Anywhere));
                    }
                    if (!anneeDeSortie.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("AnneeDeSortie", anneeDeSortie, MatchMode.Anywhere));
                    }
                    if (!acteur.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("Acteur", acteur, MatchMode.Anywhere));
                    }
                    films = criteria
                        .SetMaxResults(limit)
                        .SetFirstResult(offset)
                        .List<Film>(); ;

                    tx.Commit();
                }

                return films;
            }
        }
    }
}