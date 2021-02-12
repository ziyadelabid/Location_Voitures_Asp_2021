using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet_Asp_Location_Voitures_2021.Models;
namespace Projet_Asp_Location_Voitures_2021.Areas.Proprietaire.Controllers
{
    public class DefaultController : Controller
    {
        LocationDeVoituresEntities db = new LocationDeVoituresEntities();
        // GET: Proprietaire/Default
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.Proprietaire obj)
        {
            if (ModelState.IsValid)
            {
               
                db.Proprietaire.Add(obj);
                db.SaveChanges();
              
            }
            return RedirectToAction("Login", "Default");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Proprietaire objProp)
        {
            if (ModelState.IsValid)
            {
                using (LocationDeVoituresEntities db = new LocationDeVoituresEntities())
                {
                    var obj = db.Proprietaire.Where(a => a.Email_Prop.Equals(objProp.Email_Prop) && a.Password_Prop.Equals(objProp.Password_Prop)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["PropID"] = obj.Id_Proprietaire.ToString();
                        Session["EmailProp"] = obj.Email_Prop.ToString();
                        Session["NameProp"] = obj.Name_Prop.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objProp);
        }
        public ActionResult UserDashBoard()
        {
            


            if (Session["PropID"] != null)
            {
                string propId = Session["PropID"].ToString();
                var voitures = db.Voiture.Where(a => a.Id_Prop.ToString().Equals(propId)).ToList();
                return View(voitures);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Edit(int? id)
        {
            if (Session["PropID"] != null)
            {
               
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Voiture voiture = db.Voiture.Find(id);
                if (voiture == null)
                {
                    return HttpNotFound();
                }
                return View(voiture);
            }
            return RedirectToAction("Login");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Voiture,Marque,Couleur,Annee,Id_Prop,Prix,Promotion,Image_Voiture")] Voiture voiture)
        {
            
            if (Session["PropID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(voiture).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Userdashboard");
                }
                return View(voiture);
            }
            return RedirectToAction("Login");
        }
        public ActionResult Delete(int? id)
        {
            if (Session["PropID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Voiture voiture = db.Voiture.Find(id);
                if (voiture == null)
                {
                    return HttpNotFound();
                }
                return View(voiture);
            }
            return RedirectToAction("Login");
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["PropID"] != null)
            {
                Voiture voiture = db.Voiture.Find(id);
                db.Voiture.Remove(voiture);
                db.SaveChanges();
                return RedirectToAction("UserDashboard");
            }
            return RedirectToAction("Login");
        }
        
    }
}