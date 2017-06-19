using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientWeb.Models;
using NHibernate;
using NHibernate.Criterion;

namespace ClientWeb.DAO.Nhibernate
{
    public class ClientDao : IClientDao
    {
        public Client GetClientParCourrielEtMotDePasse(string email, string mdp)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Client client = null;
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        client = session.CreateCriteria<Client>()
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

                return client;
            }
        }
    }
}