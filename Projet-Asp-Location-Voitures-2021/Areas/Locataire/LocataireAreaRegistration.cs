using System.Web.Mvc;

namespace Projet_Asp_Location_Voitures_2021.Areas.Locataire
{
    public class LocataireAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Locataire";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Locataire_default",
                "Locataire/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}