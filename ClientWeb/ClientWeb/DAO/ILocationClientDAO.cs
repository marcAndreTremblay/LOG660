using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWeb.Models;

namespace ClientWeb.DAO
{
    interface ILocationClientDao
    {
        int GetNumberOfRentedCopiesByClientIdAndFilmId(int clientId, int filmId);
        int GetNbLocationsEnCoursByClientId(int clientId);
    }
}
