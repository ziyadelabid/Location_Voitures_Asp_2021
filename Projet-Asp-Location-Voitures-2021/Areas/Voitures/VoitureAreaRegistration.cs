using System.Web.Mvc;

namespace Projet_Asp_Location_Voitures_2021.Areas.Voitures
{
    public class VoitureAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Voitures";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Voitures_default",
                "Voitures/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}