using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientWeb.Models;
using NHibernate;
using NHibernate.Criterion;

namespace ClientWeb.DAO.Nhibernate
{
    public class EmployeDao : IEmployeDao
    {
        public Employe GetEmployeParMatriculeEtMotDePasse(string matricule, string mdp)
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