using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using ClientWeb.Models;
using NHibernate;
using NHibernate.Criterion;

namespace ClientWeb.DAO.Nhibernate
{
    public class FilmDao : IFilmDao
    {
        public IList<Film> RechercherFilmsParCriteres(string titre, string realisateur, string pays, string langueOriginale, string genre,
            string anneeSortie, string acteur, int limit, int offset)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
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

        public Film GetFilmParId(int id)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
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

        public int GetNbCopiesRestantes(int id)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                decimal nbCopiesAuTotal = 0;
                decimal nbCopiesLouees = 0;

                using (var tx = session.BeginTransaction())
                {

                    nbCopiesAuTotal = (decimal)session.CreateSQLQuery(
                        "SELECT COUNT(*) FROM Inventaire WHERE filmID = " + id).UniqueResult();

                    nbCopiesLouees = (decimal)session.CreateSQLQuery(
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

                return (int)(nbCopiesAuTotal - nbCopiesLouees);
            }
        }

        public int CountFilmsCriteres(string titre, string realisateur, string pays, string langueOriginale,
            string genre, string anneeSortie, string acteur)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                int count = 0;

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
                        criteria.Add(Restrictions.InsensitiveLike("f.LangueOriginale", langueOriginale,
                            MatchMode.Anywhere));
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
                    count = criteria.SetProjection(
                            Projections.Count(Projections.Id())
                        )
                        .UniqueResult<int>();

                    tx.Commit();
                }

                return count;
            }
        }

        public void LouerCopie(int filmId, int clientId)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
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

        public int[] GetRecommendationsForFilmId(int id)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                IList<int> recommendations;

                using (var tx = session.BeginTransaction())
                {
                    recommendations = session.CreateSQLQuery(
                        "SELECT m.correlation_value " +
                        "FROM Film f, Ma_Vue_Recommendations m" +
                        "WHERE f.filmid = " + id +
                        "AND m.id_movie = " + id
                    ).List<int>();
                }

                return recommendations.ToArray();
            }
        }
        public float GetCoteMoyenneForFilmId(int id)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                float cote = 0;

                using (var tx = session.BeginTransaction())
                {
                    cote = (float)session.CreateSQLQuery(
                        "SELECT m.average" +
                        "FROM Film f, Ma_Vue_Moyenne m" +
                        "WHERE f.filmid = " + id +
                        "AND m.id_movie = " + id
                    ).UniqueResult();
                }

                return cote;
            }
        }
    }
}