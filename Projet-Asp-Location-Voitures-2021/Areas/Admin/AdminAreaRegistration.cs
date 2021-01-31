using System.Web.Mvc;

namespace Projet_Asp_Location_Voitures_2021.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
               new[] { "Projet_Asp_Location_Voitures_2021.Areas.Admin.Controllers" }


            );
        }
    }
}