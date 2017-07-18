using NHibernate;
using NHibernate.Criterion;

namespace ClientWeb.Models
{
    public class Employe
    {
        public virtual Adresse Adresse { get; set; }
        public virtual string Courriel { get; set; }
        public virtual int Id { get; set; }
        public virtual string Matricule { get; set; }
        public virtual string NumeroTel { get; set; }
        public virtual string Password { get; set; }
        public virtual Personne Personne { get; set; }

        public static Employe TrouverEmployeParMatriculeEtMotDePasse(string matricule, string mdp)// a faire
        {
            using (ISession session = ClientSession.GetClientSession().OpenSession())
            {
                Employe UnEmploye = null;
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        UnEmploye = session.CreateCriteria<Employe>()
                           .Add(Restrictions.Eq("Matricule", matricule))
                           .Add(Restrictions.Eq("Password", mdp))
                           .UniqueResult<Employe>();

                        tx.Commit();
                    }
                    catch (NonUniqueResultException)
                    {
                        return null;
                    }
                }

                return UnEmploye;
            }
        }
    }
}