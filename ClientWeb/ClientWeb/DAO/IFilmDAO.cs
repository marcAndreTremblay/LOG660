using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWeb.Models;

namespace ClientWeb.DAO
{
    interface IFilmDao
    {
        IList<Film> RechercherFilmsParCriteres(string titre, string realisateur, string pays, string langueOriginale,
            string genre, string anneeSortie, string acteur, int limit, int offset);
		int CountFilmsCriteres(string titre, string realisateur, string pays, string langueOriginale, string genre, string anneeSortie, string acteur);
        Film GetFilmParId(int id);
        int GetNbCopiesRestantes(int id);
        IList<string> GetRecommendationsForFilmId(int id);
        float GetCoteMoyenneForFilmId(int id);
        void LouerCopie(int filmId, int clientId);
    }
}
