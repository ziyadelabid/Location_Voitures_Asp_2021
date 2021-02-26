using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
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
        public ActionResult Index(Models.Proprietaire obj, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                string fileName = System.IO.Path.GetFileName(postedFile.FileName);
               
                //Set the Image File Path.
                var filePath=  Path.Combine(Server.MapPath("~/images"), fileName);
                

                //Save the Image File in Folder.
                postedFile.SaveAs(filePath);

                //Insert the Image File details in Table.
                LocationDeVoituresEntities entities = new LocationDeVoituresEntities();
                obj.Image_Name = fileName;
                obj.Image_Prop = filePath;

                db.Proprietaire.Add(obj);
                db.SaveChanges();
              
            }
            return RedirectToAction("Login", "Default");

        }
        public ActionResult AddCar()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar([Bind(Include = "Id_Voiture,Marque,Couleur,Annee,Prix,Promotion,Image_Voiture,Carburant,Boite_Vitesse,Emplacement_Prise,Emplacement_Retour,Places,Portes,Image_Name")] Voiture voiture, HttpPostedFileBase imageVoiture)
        {
            if (Session["PropID"] != null)
            {
                if (ModelState.IsValid)
                {
                    string fileName = System.IO.Path.GetFileName(imageVoiture.FileName);

                    //Set the Image File Path.
                    var filePath = Path.Combine(Server.MapPath("~/images"), fileName);


                    //Save the Image File in Folder.
                    imageVoiture.SaveAs(filePath);

                    //Insert the Image File details in Table.
                    LocationDeVoituresEntities entities = new LocationDeVoituresEntities();
                    voiture.Image_Name = fileName;
                    voiture.Image_Voiture = filePath;

                    voiture.Promotion = Convert.ToInt16(voiture.Prix -(voiture.Prix*voiture.Promotion/100));
                    voiture.Id_Prop = Int32.Parse(Session["PropID"].ToString());
                    db.Voiture.Add(voiture);
                    db.SaveChanges();
                    return RedirectToAction("UserDashboard");
                }

                return View(voiture);
            }
            else
            {
                return RedirectToAction("Login");
            }
           
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
        public ActionResult Edit([Bind(Include = "Id_Voiture,Marque,Couleur,Annee,Id_Prop,Prix,Promotion,Image_Voiture,Image_Name,Carburant,Boite_Vitesse,Emplacement_Prise,Emplacement_Retour,Places,Portes")] Voiture voiture)
        {
            
            if (Session["PropID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(voiture).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UserDashboard");
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
        //Edit Profil du proprietaire
        public ActionResult EditProfil(int? id)
        {
            if (Session["PropID"] != null)
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
            return RedirectToAction("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfil([Bind(Include = "Id_Proprietaire,Name_Prop,Email_Prop,Password_Prop,Phone_number_Prop,Adresse_Prop,Image_Prop,Role,Type,Image_Name")] Models.Proprietaire proprietaire)
        {

            if (Session["PropID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(proprietaire).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UserDashboard");
                }
                return View(proprietaire);
            }
            return RedirectToAction("Login");
        }
        public ActionResult AddReclamation()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddReclamation(Reclamation reclamation)
        {
            try
            {
               
                reclamation.Date_Reclamation = DateTime.Now;
                reclamation.Id_Prop = Int32.Parse(Session["PropID"].ToString());
                db.Reclamation.Add(reclamation);
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

            return RedirectToAction("UserDashboard");

        }

    }
}