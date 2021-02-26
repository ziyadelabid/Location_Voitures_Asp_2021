using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projet_Asp_Location_Voitures_2021.Models;


namespace Projet_Asp_Location_Voitures_2021.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        private LocationDeVoituresEntities db = new LocationDeVoituresEntities();
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListProprietaire()
        {
            return View(db.Proprietaire.ToList());
        }
    }
}