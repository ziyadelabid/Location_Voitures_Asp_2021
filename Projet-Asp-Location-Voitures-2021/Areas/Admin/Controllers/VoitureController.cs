using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projet_Asp_Location_Voitures_2021.Models;
namespace Projet_Asp_Location_Voitures_2021.Areas.Admin.Controllers
{
    public class VoitureController : Controller
    {
        List<Voiture> listVoitures = new List<Voiture>
       {
           new Voiture
           {
               Id=4,
               Marque="Mercedes",
               Couleur="Rouge",
           },
            new Voiture
           {
               Id=5,
               Marque="Dacia",
               Couleur="Noire",
           }
       };
        // GET: Admin/Voiture
        public ActionResult Index()
        {
            return View(listVoitures);
        }
    }
}