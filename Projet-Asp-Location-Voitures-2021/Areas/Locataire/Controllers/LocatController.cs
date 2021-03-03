using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet_Asp_Location_Voitures_2021.Models;
namespace Projet_Asp_Location_Voitures_2021.Areas.Locataire.Controllers
{
    public class LocatController : Controller
{
    private LocationDeVoituresEntities db = new LocationDeVoituresEntities();
    // GET: Locataire/Locat
    public ActionResult Index(Models.Locataire locat)
    {
        return View(db.Locataire.ToList());
    }
    [HttpPost]
    public ActionResult Index(Models.Locataire obj, HttpPostedFileBase postedFile)
    {
        if (ModelState.IsValid)
        {
            db.Locataire.Add(obj);
            db.SaveChanges();
        }
        return RedirectToAction("LoginLoc", "Locat");

    }

    // GET: Locataire/LocatProfil/Details/5
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

    // GET: Locataire/LocatProfil/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Locataire/LocatProfil/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id_Locataire,Email_Loc,Password_Loc,Phone_number_Loc,Adresse_Loc,Name_Loc,Cin_Loc,Permis,Role,image_Loc")] Models.Locataire locat)
    {
        if (ModelState.IsValid)
        {
            db.Locataire.Add(locat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(locat);
    }



    // POST: Locataire/LocatProfil/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id_Locataire,Email_Loc,Password_Loc,Phone_number_Loc,Adresse_Loc,Name_Loc,Cin_Loc,Permis,Role,image_Loc")] Models.Locataire locat)
    {
        if (ModelState.IsValid)
        {
            db.Entry(locat).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(locat);
    }


    //Edit Profil du proprietaire
    public ActionResult EditProfil(int? id)
    {
        if (Session["LocID"] != null)
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
        return RedirectToAction("Login");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditProfil([Bind(Include = "Id_Locataire,Email_Loc,Password_Loc,Phone_number_Loc,Adresse_Loc,Name_Loc,Cin_Loc,Permis,Role,image_Loc")] Models.Locataire locat)
    {

        if (Session["LocID"] != null)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DashboardLoc");
            }
            return View(locat);
        }
        return RedirectToAction("Login");
    }

    // GET: Locataire/LocatProfil/Delete/5
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

    // POST: Locataire/LocatProfil/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
            Models.Locataire locat = db.Locataire.Find(id);
        db.Locataire.Remove(locat);
        db.SaveChanges();
        return RedirectToAction("Index");
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
                    return RedirectToAction("DashboardLoc");
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
    public ActionResult ReservationPage(Reservation reservation, int id, Voiture v)
    {

        try
        {
            reservation.Id_Loc = Int32.Parse(Session["LocID"].ToString());
            reservation.Id_Voit = id;
            reservation.Montant = Convert.ToDecimal(db.Voiture.Find(id).Promotion);
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

    public ActionResult MesReserve()
    {
        if (Session["LocID"] != null)
        {
            return View(db.Reservation.ToList());
        }
        return RedirectToAction("LoginLoc");
    }

    //Get :EditReservation 
    public ActionResult EditReservation(int? id)
    {
        if (Session["LocID"] != null)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation res = db.Reservation.Find(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }
        return RedirectToAction("Login");
    }

    //Post:EditReservation
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditReservation([Bind(Include = "Id_Reservation,Date_Reservation,Date_retour,Montant,Id_Loc,Id_Voit")] Reservation res)
    {

        if (Session["LocID"] != null)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Id_Loc = Session["LocID"].ToString();
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MesReserve");
            }
            return View(res);
        }
        return RedirectToAction("Login");
    }











    // GET: Locataire/Reservations1/Edit/5
    public ActionResult EditRes(int? id)
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
    public ActionResult EditRes([Bind(Include = "Id_Reservation,Date_Reservation,Date_retour,Montant,Id_Loc,Id_Voit")] Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            db.Entry(reservation).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MesReserve");
        }
        ViewBag.Id_Loc = new SelectList(db.Locataire, "Id_Locataire", "Email_Loc", reservation.Id_Loc);
        ViewBag.Id_Voit = new SelectList(db.Voiture, "Id_Voiture", "Marque", reservation.Id_Voit);
        return View(reservation);
    }


    // GET: Locataire/Reservations1/Delete/5
    public ActionResult DeleteRes(int? id)
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
    [HttpPost, ActionName("DeleteRes")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteResConfirmed(int id)
    {
        if (Session["LocID"] != null)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("MesReserve");
        }
        return RedirectToAction("MesReserve");
    }


}
}