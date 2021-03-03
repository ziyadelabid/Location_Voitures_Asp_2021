using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projet_Asp_Location_Voitures_2021.Models;


namespace Projet_Asp_Location_Voitures_2021.Areas.Admin.Services
{
    public class UserService
    {
        readonly  LocationDeVoituresEntities VoitureDB = new LocationDeVoituresEntities();


        public void SetFavouriteProp(int id)
        {
            Models.Proprietaire proprietaire = VoitureDB.Proprietaire.Find(id);

            VoitureDB.Entry(proprietaire).State = EntityState.Modified;
            proprietaire.Favourite = true;
            VoitureDB.SaveChanges();
        }


        public void UnSetFavouriteProp(int id)
        {
            Models.Proprietaire proprietaire = VoitureDB.Proprietaire.Find(id);

            VoitureDB.Entry(proprietaire).State = EntityState.Modified;
            proprietaire.Favourite = false;
            VoitureDB.SaveChanges();
        }

        public void SetBanProp(int id)
        {
            Models.Proprietaire proprietaire = VoitureDB.Proprietaire.Find(id);

            VoitureDB.Entry(proprietaire).State = EntityState.Modified;
            proprietaire.Blocked = true;
            VoitureDB.SaveChanges();
        }

        public void UnSetBanProp(int id)
        {
            Models.Proprietaire proprietaire = VoitureDB.Proprietaire.Find(id);

            VoitureDB.Entry(proprietaire).State = EntityState.Modified;
            proprietaire.Blocked = false;
            VoitureDB.SaveChanges();
        }


        public void SetFavouriteLocat(int id)
        {
            Models.Locataire locataire = VoitureDB.Locataire.Find(id);

            VoitureDB.Entry(locataire).State = EntityState.Modified;
            locataire.Favourite = true;
            VoitureDB.SaveChanges();
        }


        public void UnSetFavouriteLocat(int id)
        {
            Models.Locataire locataire = VoitureDB.Locataire.Find(id);

            VoitureDB.Entry(locataire).State = EntityState.Modified;
            locataire.Favourite = false;
            VoitureDB.SaveChanges();
        }

        public void SetBanLocat(int id)
        {
            Models.Locataire locataire = VoitureDB.Locataire.Find(id);

            VoitureDB.Entry(locataire).State = EntityState.Modified;
            locataire.Blocked = true;
            VoitureDB.SaveChanges();
        }

        public void UnSetBanLocat(int id)
        {
            Models.Locataire locataire = VoitureDB.Locataire.Find(id);

            VoitureDB.Entry(locataire).State = EntityState.Modified;
            locataire.Blocked = false;
            VoitureDB.SaveChanges();
        }
    }
}