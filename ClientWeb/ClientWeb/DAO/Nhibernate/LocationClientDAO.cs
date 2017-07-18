using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientWeb.Models;
using NHibernate;
using NHibernate.Criterion;

namespace ClientWeb.DAO.Nhibernate
{
    public class LocationClientDao : ILocationClientDao
    {
        public int GetNumberOfRentedCopiesByClientIdAndFilmId(int clientId, int filmId)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                int count = 0;

                using (var tx = session.BeginTransaction())
                {
                    count = (int)session.CreateCriteria<LocationClient>("lc")
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

        public int GetNbLocationsEnCoursByClientId(int clientId)
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                int nbCopiesLouees = 0;
                using (var tx = session.BeginTransaction())
                {
                    nbCopiesLouees = (int)session.CreateCriteria<LocationClient>()
                        .Add(Restrictions.Eq("Client.Id", clientId))
                        .Add(Restrictions.Or(
                            Restrictions.Eq("DateRetour", Convert.ToDateTime("0001-01-01")),
                            Restrictions.IsNull("DateRetour")
                        ))
                        .SetProjection(Projections.CountDistinct("Id"))
                        .UniqueResult();

                    tx.Commit();
                }

                return nbCopiesLouees;
            }
        }
    }
}