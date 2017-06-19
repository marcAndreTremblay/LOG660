using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWeb.Models;

namespace ClientWeb.DAO
{
    interface IClientDao
    {
        Client GetClientParCourrielEtMotDePasse(string email, string mdp);
    }
}
