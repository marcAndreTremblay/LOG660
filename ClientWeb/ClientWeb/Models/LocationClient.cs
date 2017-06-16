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

        public static LocationClient GetLocationByClientIdAndFilmId(int clientId, int filmId)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                LocationClient lc = null;
                List<LocationClient> locations = null ;

                using (var tx = session.BeginTransaction())
                {
                    lc = session.CreateCriteria<LocationClient>()
                        .Add(Restrictions.Eq("Client.Id", clientId))
                        //.Add(Restrictions.Eq("Inventaire.Film.Id", filmId))
                        .Add(Restrictions.IsNull("DateRetour"))
                        .UniqueResult<LocationClient>();

                    tx.Commit();
                }

                return lc;
            }
        }
    }
}