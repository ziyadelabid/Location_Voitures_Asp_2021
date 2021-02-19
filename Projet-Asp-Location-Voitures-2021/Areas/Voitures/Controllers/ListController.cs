using PagedList;
using Projet_Asp_Location_Voitures_2021.Areas.Voitures.DTOs;
using Projet_Asp_Location_Voitures_2021.Areas.Voitures.Models;
using Projet_Asp_Location_Voitures_2021.Areas.Voitures.Services;
using Projet_Asp_Location_Voitures_2021.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projet_Asp_Location_Voitures_2021.Areas.Voitures.Controllers
{
    public class ListController : Controller
    {

        private readonly VoitureService voitureService = new VoitureService();


        [HttpGet]
        public ActionResult Index(int? page,string DisponibleFilter)
        {

            ViewBag.prixItems = voitureService.getFilterDropDownItems()["prix"];
            ViewBag.carburantItems = voitureService.getFilterDropDownItems()["carburant"];
            ViewBag.marqueItems = voitureService.getFilterDropDownItems()["marque"];
            ViewBag.boiteVitesseItems = voitureService.getFilterDropDownItems()["boiteVitesse"];

            if (DisponibleFilter == FilterEnum.DISPONIBLES.ToString())
            {
                ViewBag.SelectedValue = FilterEnum.DISPONIBLES.ToString();
                IPagedList<VoitureModel> list = voitureService.SortedVoitures(page);
                return View(list);
            }

            ViewBag.SelectedValue = FilterEnum.ALL.ToString();

            return View(voitureService.GetAll(page));
           
        }
        
        
        [HttpPost]
        public ActionResult FilteredVoitures(VoitureSearchFilter filterVoiture)
        {
            return PartialView("FilteredVoitures",voitureService.FilteredVoitures(filterVoiture));
        }


    }
}