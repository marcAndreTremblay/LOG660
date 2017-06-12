using System.Web;

namespace ClientWeb.Models
{
    public static class GestionConnexion
    {
        public static bool estConnecte()
        {
            return HttpContext.Current.Session["UtilisateurConnecté"] != null;
        }
    }
}