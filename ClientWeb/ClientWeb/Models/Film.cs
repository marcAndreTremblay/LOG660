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
                    ICriteria criteria = session.CreateCriteria<Film>("f");

                    if (!titre.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("f.Titre", titre, MatchMode.Anywhere));
                    }
                    if (!realisateur.IsEmpty())
                    {
                        criteria.CreateCriteria("f.Realisateurs", "r").CreateCriteria("r.Personne", "pr")
                            .Add(Restrictions.Or(
                                Restrictions.InsensitiveLike("pr.NomFamille", realisateur, MatchMode.Anywhere),
                                Restrictions.InsensitiveLike("pr.Prenom", realisateur, MatchMode.Anywhere)
                            ));
                    }
                    if (!pays.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("f.Pays", pays, MatchMode.Anywhere));
                    }
                    if (!langueOriginale.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("f.LangueOriginale", langueOriginale, MatchMode.Anywhere));
                    }
                    if (!genre.IsEmpty())
                    {
                        criteria.Add(Restrictions.InsensitiveLike("f.Genres", genre, MatchMode.Anywhere));
                    }
                    if (!anneeSortie.IsEmpty())
                    {
                        criteria.Add(Restrictions.Eq("f.AnneeSortie", Int32.Parse(anneeSortie)));
                    }
                    if (!acteur.IsEmpty())
                    {
                        criteria.CreateCriteria("f.FilmActeurs", "fm").CreateCriteria("fm.Personne", "pa")
                            .Add(Restrictions.Or(
                                Restrictions.InsensitiveLike("pa.Prenom", acteur, MatchMode.Anywhere),
                                Restrictions.InsensitiveLike("pa.NomFamille", acteur, MatchMode.Anywhere)
                            ));
                    }
                    films = criteria
                        .SetMaxResults(limit)
                        .SetFirstResult(offset)
                        .SetProjection(
                            Projections.Distinct(Projections.ProjectionList()
                                .Add(Projections.Alias(Projections.Property("Titre"), "Titre"))
                                .Add(Projections.Alias(Projections.Property("AnneeSortie"), "AnneeSortie"))
                                .Add(Projections.Alias(Projections.Property("LangueOriginale"), "LangueOriginale"))
                                .Add(Projections.Alias(Projections.Property("DureeMinutes"), "DureeMinutes"))
                                .Add(Projections.Alias(Projections.Property("Id"), "Id"))
                            )
                        )
                        .SetResultTransformer(
                            new NHibernate.Transform.AliasToBeanResultTransformer(typeof(Film)))
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

        public static int GetNbCopiesRestantes(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                decimal nbCopiesAuTotal = 0;
                decimal nbCopiesLouees = 0;

                using (var tx = session.BeginTransaction())
                {
                    
                    nbCopiesAuTotal = (decimal) session.CreateSQLQuery(
                        "SELECT COUNT(*) FROM Inventaire WHERE filmID = " + id).UniqueResult();

                    nbCopiesLouees = (decimal) session.CreateSQLQuery(
                        "SELECT COUNT(*) " +
                        "FROM Location_Client " +
                        "WHERE (dateRetour = '01-01-01' OR dateRetour IS NULL) " +
                        "AND codeCopieID = ANY (" +
                            "SELECT codeCopieID " +
                            "FROM Inventaire " +
                            "WHERE filmID = " + id +
                        ")"
                    ).UniqueResult();

                    tx.Commit();
                }

                return (int) (nbCopiesAuTotal - nbCopiesLouees);
            }
        }

        public static void LouerCopie(int filmId, int clientId)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    IQuery query = session.GetNamedQuery("PLOUERFILM");
                   query.SetInt32("filmID", filmId);
                    query.SetInt32("clientID", clientId);
                    query.UniqueResult();

                    tx.Commit();
                }
            }
        }
    }
}