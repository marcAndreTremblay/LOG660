using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace ClientWeb.Models
{
    public class LocationClient
    {
        public virtual Client Client { get; set; }
        public virtual DateTime DateLocation { get; set; }
        public virtual DateTime DateRetour { get; set; }
        public virtual int Id { get; set; }
        public virtual Inventaire Inventaire { get; set; }

        public static int GetNumberOfRentedCopiesByClientIdAndFilmId(int clientId, int filmId)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                int count = 0;

                using (var tx = session.BeginTransaction())
                {
                    count = (int) session.CreateCriteria<LocationClient>("lc")
                        .CreateCriteria("lc.Inventaire", "i")
                        .CreateCriteria("i.Film", "f")
                        .Add(Restrictions.Eq("lc.Client.Id", clientId))
                        .Add(Restrictions.Eq("f.Id", filmId))
                        .Add(Restrictions.Or(
                            Restrictions.Eq("lc.DateRetour", Convert.ToDateTime("0001-01-01")),
                            Restrictions.IsNull("lc.DateRetour")
                        ))
                        .SetProjection(Projections.RowCount())
                        .UniqueResult();

                    tx.Commit();
                }

                return count;
            }
        }
    }
}