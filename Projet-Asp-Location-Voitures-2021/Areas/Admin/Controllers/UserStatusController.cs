using Projet_Asp_Location_Voitures_2021.Areas.Admin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projet_Asp_Location_Voitures_2021.Areas.Admin.Controllers
{
    public class UserStatusController : Controller
    {

        UserService userService = new UserService();

        // GET: Admin/Favourites
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FavoriteProp(int id,string status)
        {

            if (status.Equals("set"))
            {
                userService.SetFavouriteProp(id);
            }
            else if(status.Equals("unset"))
            {
                userService.UnSetFavouriteProp(id);
            }

            return RedirectToAction("ListProp","Prop",new { Area = "Admin" });
        }

        public ActionResult BanProp(int id,string status)
        {
            if (status.Equals("set"))
            {
                userService.SetBanProp(id);
            }
            else if (status.Equals("unset"))
            {
                userService.UnSetBanProp(id);
            }

            return RedirectToAction("ListProp", "Prop", new { Area = "Admin" });
        }


        public ActionResult FavoriteLocat(int id, string status)
        {

            if (status.Equals("set"))
            {
                userService.SetFavouriteLocat(id);
            }
            else if (status.Equals("unset"))
            {
                userService.UnSetFavouriteLocat(id);
            }

            return RedirectToAction("Index", "InfoClient", new { Area = "Admin" });
        }

        public ActionResult BanLocat(int id, string status)
        {
            if (status.Equals("set"))
            {
                userService.SetBanLocat(id);
            }
            else if (status.Equals("unset"))
            {
                userService.UnSetBanLocat(id);
            }

            return RedirectToAction("Index", "InfoClient", new { Area = "Admin" });
        }

    }
}