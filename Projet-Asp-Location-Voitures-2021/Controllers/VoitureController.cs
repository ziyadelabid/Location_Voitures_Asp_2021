using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projet_Asp_Location_Voitures_2021.Models;

namespace Projet_Asp_Location_Voitures_2021.Controllers
{
    public class VoitureController : Controller
    {
        locationVoituresEntities dbContext = new locationVoituresEntities();
        List<Voiture> listVoitures = new List<Voiture>
       {
           new Voiture
           {
               Id=1,
               Marque="Mercedes",
               Couleur="Rouge",
           },
            new Voiture
           {
               Id=2,
               Marque="Dacia",
               Couleur="Noire",
           }
       };
        
        // GET: Voiture
        public ActionResult Index()
        {

            return View(listVoitures);
        }
       
    }
}