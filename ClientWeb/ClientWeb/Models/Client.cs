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

        public static Client TrouverClientParCourrielEtMotDePasse(string email, string mdp)// a faire
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Client UnClient = null;
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        UnClient = session.CreateCriteria<Client>()
                           .Add(Restrictions.Eq("Courriel", email))
                           .Add(Restrictions.Eq("Password", mdp))
                           .UniqueResult<Client>();

                        tx.Commit();
                    }
                    catch (NonUniqueResultException)
                    {
                        return null;
                    }
                }

                return UnClient;
            }
        }

        public virtual int GetNbLocationsEnCours()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                int nbCopiesLouees = 0;
                using (var tx = session.BeginTransaction())
                {
                    nbCopiesLouees = (int) session.CreateCriteria<LocationClient>()
                        .Add(Restrictions.Eq("Client.Id", Id))
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