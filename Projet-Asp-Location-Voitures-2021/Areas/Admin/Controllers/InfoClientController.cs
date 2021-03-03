using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet_Asp_Location_Voitures_2021.Models;

namespace Projet_Asp_Location_Voitures_2021.Areas.Admin.Controllers
{
    public class InfoClientController : Controller
    {
        private LocationDeVoituresEntities db = new LocationDeVoituresEntities();

        // GET: Admin/InfoClient
        public ActionResult Index()
        {
            return View(db.Locataire.ToList());
        }

        // GET: Admin/InfoClient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Locataire locat = db.Locataire.Find(id);
            if (locat == null)
            {
                return HttpNotFound();
            }
            return View(locat);
        }



        // GET: Admin/InfoClient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Locataire locat = db.Locataire.Find(id);
            if (locat == null)
            {
                return HttpNotFound();
            }
            return View(locat);
        }

        // POST: Admin/InfoClient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Locataire,Email_Loc,Password_Loc,Phone_number_Loc,Adresse_Loc,Name_Loc,Cin_Loc,Permis,Role,image_Loc")]  Models.Locataire locat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locat);
        }

        // GET: Admin/InfoClient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Locataire locat = db.Locataire.Find(id);
            if (locat == null)
            {
                return HttpNotFound();
            }
            return View(locat);
        }

        // POST: Admin/InfoClient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Locataire locat = db.Locataire.Find(id);
            db.Locataire.Remove(locat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public ActionResult LocReserve()
        {
            return View(db.Reservation.ToList());
        }


        // GET: Locataire/Reservations1/Edit/5
        public ActionResult EditResLoc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Loc = new SelectList(db.Locataire, "Id_Locataire", "Email_Loc", reservation.Id_Loc);
            ViewBag.Id_Voit = new SelectList(db.Voiture, "Id_Voiture", "Marque", reservation.Id_Voit);
            return View(reservation);
        }

        // POST: Locataire/Reservations1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResLoc([Bind(Include = "Id_Reservation,Date_Reservation,Date_retour,Montant,Id_Loc,Id_Voit")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LocReserve");
            }
            ViewBag.Id_Loc = new SelectList(db.Locataire, "Id_Locataire", "Email_Loc", reservation.Id_Loc);
            ViewBag.Id_Voit = new SelectList(db.Voiture, "Id_Voiture", "Marque", reservation.Id_Voit);
            return View(reservation);
        }


        // GET: Locataire/Reservations1/Delete/5
        public ActionResult DeleteResLoc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Locataire/Reservations1/Delete/5
        [HttpPost, ActionName("DeleteResLoc")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteResConfirmed(int id)
        {

            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("LocReserve");

        }



    }
}