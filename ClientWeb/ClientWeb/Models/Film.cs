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
                        .List<Film>();

                    tx.Commit();
                }

                return films;
            }
        }

        public static IList<Film> RechercherFilmsParCriteres(string titre, string realisateur, string pays, string langueOriginale, string genre, string anneeSortie, string acteur, int limit, int offset)
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
                    if (!anneeSortie.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("Annee", anneeSortie, MatchMode.Anywhere));
                    }
                    if (!acteur.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("FilmActeur.Personne.Prenom", acteur, MatchMode.Anywhere));
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

        public static Film ChercherFilmParId(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Film film = null;

                using (var tx = session.BeginTransaction())
                {
                    film = session.Get<Film>(id);

                    tx.Commit();
                }

                return film;
            }
        }
    }
}