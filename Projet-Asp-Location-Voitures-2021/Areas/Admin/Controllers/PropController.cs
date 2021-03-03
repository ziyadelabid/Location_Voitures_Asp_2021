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
    public class PropController : Controller
    {
        private LocationDeVoituresEntities db = new LocationDeVoituresEntities();
        // GET: Admin/Prop
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListProp()
        {
            return View(db.Proprietaire.ToList());
        }
        public ActionResult EditProp(int? id)
        {
           

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Models.Proprietaire proprietaire = db.Proprietaire.Find(id);
                if (proprietaire == null)
                {
                    return HttpNotFound();
                }
            return View(proprietaire);
        }
                
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProp([Bind(Include = "Id_Proprietaire,Name_Prop,Email_Prop,Password_Prop,Phone_number_Prop,Adresse_Prop,Image_Prop,Role,Type,Image_Name")] Models.Proprietaire proprietaire)
        {

            
                if (ModelState.IsValid)
                {
                    db.Entry(proprietaire).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ListProp");
                }
                return View(proprietaire);
            }

        public ActionResult DeleteProp(int? id)
        {
            
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Models.Proprietaire prop = db.Proprietaire.Find(id);
                if (prop == null)
                {
                    return HttpNotFound();
                }
                return View(prop);
            }
            
        [HttpPost, ActionName("DeleteProp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
                Models.Proprietaire prop = db.Proprietaire.Find(id);
                db.Proprietaire.Remove(prop);
                db.SaveChanges();
                return RedirectToAction("ListProp");
         }
        public ActionResult ListVoiture(int id)
        {
            var voitures = db.Voiture.Where(a => a.Id_Prop.Equals(id)).ToList();
            return View(voitures);
           
        }

        public ActionResult EditVoiture(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Voiture voiture = db.Voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVoiture([Bind(Include = "Id_Voiture,Marque,Couleur,Annee,Id_Prop,Prix,Promotion,Image_Voiture,Image_Name,Carburant,Boite_Vitesse,Emplacement_Prise,Emplacement_Retour,Places,Portes")] Models.Voiture voitures)
        {


            if (ModelState.IsValid)
            {
                db.Entry(voitures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListVoiture", new { id = voitures.Id_Prop });
            }
            return View(voitures);
        }
        public ActionResult DeleteVoiture(int? id)
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

        [HttpPost, ActionName("DeleteVoiture")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirme(int id)
        {

            Models.Voiture voiture = db.Voiture.Find(id);
            db.Voiture.Remove(voiture);
            db.SaveChanges();
            return RedirectToAction("ListVoiture", new { id=voiture.Id_Prop});
        }
        
        public ActionResult ListReclamation()
        {
           
            return View(db.Reclamation.ToList());
        }

    }
}