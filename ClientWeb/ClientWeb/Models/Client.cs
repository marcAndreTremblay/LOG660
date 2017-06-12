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
        public virtual List<LocationClient> locations { get; set; }
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
            int nb = 0;
            foreach (LocationClient l in locations)
            {
                if (l.DateRetour == null)
                    nb++;
            }
            return nb;
        }
    }
}