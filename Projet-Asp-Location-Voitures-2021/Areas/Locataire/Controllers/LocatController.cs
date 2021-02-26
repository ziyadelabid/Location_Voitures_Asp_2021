using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projet_Asp_Location_Voitures_2021.Models;
    namespace Projet_Asp_Location_Voitures_2021.Areas.Locataire.Controllers
{
    public class LocatController : Controller
    {
        private LocationDeVoituresEntities db = new LocationDeVoituresEntities();
        // GET: Locataire/Locat
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginLoc()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginLoc(Models.Locataire objLoc)
        {
            if (ModelState.IsValid)
            {
                using (LocationDeVoituresEntities db = new LocationDeVoituresEntities())
                {
                    var obj = db.Locataire.Where(a => a.Email_Loc.Equals(objLoc.Email_Loc) && a.Password_Loc.Equals(objLoc.Password_Loc)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["LocID"] = obj.Id_Locataire.ToString();
                        Session["EmailLoc"] = obj.Email_Loc.ToString();
                        Session["NameLoc"] = obj.Name_Loc.ToString();
                        return  RedirectToAction("DashboardLoc"); 
                    }
                }
            }
            return View(objLoc);
        }
        public ActionResult DashboardLoc()
        {



            if (Session["LocID"] != null)
            {
                string locId = Session["LocID"].ToString();
                var voitures = db.Voiture.ToList();
                return View(voitures);
            }
            else
            {
                return RedirectToAction("LoginLoc");
            }
        }
        public ActionResult ReservationPage()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ReservationPage(Reservation reservation, int id)
        {
         
            try
            {
                reservation.Id_Loc = Int32.Parse(Session["LocID"].ToString());
                reservation.Id_Voit = id;
                db.Reservation.Add(reservation);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("DashboardLoc");



        }

    }
}