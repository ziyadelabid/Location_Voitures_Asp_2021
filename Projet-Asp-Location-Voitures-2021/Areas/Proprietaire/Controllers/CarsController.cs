using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projet_Asp_Location_Voitures_2021.Models;
namespace Projet_Asp_Location_Voitures_2021.Areas.Proprietaire.Controllers
{
    public class CarsController : Controller
    {
        private LocationDeVoituresEntities db = new LocationDeVoituresEntities();
        // GET: Proprietaire/Cars
        public ActionResult Index()
        {
      
            return View(db.Voiture.ToList());
        }
    }
}