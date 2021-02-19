using Projet_Asp_Location_Voitures_2021.Areas.Voitures.Models;
using Projet_Asp_Location_Voitures_2021.Areas.Voitures.Services;
using Projet_Asp_Location_Voitures_2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projet_Asp_Location_Voitures_2021.Controllers
{
    public class DefaultController : Controller
    {

        VoitureService voitureService = new VoitureService();

        // GET: Default
        public ActionResult Index()
        {
            List<VoitureModel> promotionVoitures = voitureService.GetCarsWithPromotion();
            List<VoitureModel> featuredVoitures = voitureService.GetMostUsedCars();
            Tuple<int, int, int,int> statistics = voitureService.GetStatistics();

            IndexViewModel indexViewModel = new IndexViewModel()
            {
                featuredVoitures = featuredVoitures,
                promotionVoitures = promotionVoitures,
                NombreMarques = statistics.Item2,
                NombreVoitures = statistics.Item1,
                NombreReservationsParMois = statistics.Item3,
                NombreClients = statistics.Item4
            };
            
            return View(indexViewModel);
        }
    }
}