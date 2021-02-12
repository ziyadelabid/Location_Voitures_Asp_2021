using System.Web.Mvc;

namespace Projet_Asp_Location_Voitures_2021.Areas.Proprietaire
{
    public class ProprietaireAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Proprietaire";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Proprietaire_default",
                "Proprietaire/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}