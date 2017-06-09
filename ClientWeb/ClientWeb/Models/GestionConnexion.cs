using System.Web;

namespace ClientWeb.Models
{
    public static class GestionConnexion
    {
        public static bool estConnecte()
        {
            if (HttpContext.Current.Session["EstConnecté"] == null)
                HttpContext.Current.Session["EstConnecté"] = false;
            return (bool)HttpContext.Current.Session["EstConnecté"];
        }
    }
}