using ClientWeb.Models;

namespace ClientWeb.DAO
{
    public interface IEmployeDao
    {
        Employe GetEmployeParMatriculeEtMotDePasse(string matricule, string mdp);
    }
}