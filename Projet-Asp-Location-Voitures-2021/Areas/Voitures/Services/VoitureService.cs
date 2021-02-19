using LinqKit;
using PagedList;
using Projet_Asp_Location_Voitures_2021.Areas.Voitures.DTOs;
using Projet_Asp_Location_Voitures_2021.Areas.Voitures.Models;
using Projet_Asp_Location_Voitures_2021.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projet_Asp_Location_Voitures_2021.Areas.Voitures.Services
{
    public class VoitureService
    {
        private LocationDeVoituresEntities voitureDB = new LocationDeVoituresEntities();
        private readonly int PageSize = 4;

        public IPagedList<VoitureModel> GetAll(int? pageNumber)
        {
           
            return (from voiture in voitureDB.Voiture
                    from reservation in voitureDB.Reservation
                    .Where(res => res.Id_Voit == voiture.Id_Voiture)
                    .DefaultIfEmpty()
                    select new VoitureModel()
                    {
                        Id = voiture.Id_Voiture,
                        Marque = voiture.Marque,
                        Annee = voiture.Annee,
                        Couleur = voiture.Couleur,
                        Image_Name = voiture.Image_Name,
                        Prix = voiture.Prix,
                        Promotion = voiture.Promotion,
                        Places = voiture.Places,
                        Portes = voiture.Portes,
                        Carburant = voiture.Carburant,
                        Boite_Vitesse = voiture.Boite_Vitesse,
                        DateRetour = reservation.Date_Retour,
                        DateReservation = reservation.Date_Reservation
                    })
                    .OrderBy(a => a.Id).ToPagedList(pageNumber ?? 1,PageSize);
        }

        public IPagedList<VoitureModel> SortedVoitures(int? pageNumber)
        {
            return (from voiture in voitureDB.Voiture
                    join reservation in voitureDB.Reservation
                    on voiture.Id_Voiture equals reservation.Id_Voit
                    into tempTable
                    from temp in tempTable.DefaultIfEmpty()
                    where temp.Id_Voit == null || temp.Date_Retour < DateTime.Now || temp.Date_Reservation > DateTime.Now
                    select new VoitureModel()
                    {
                        Id = voiture.Id_Voiture,
                        Marque = voiture.Marque,
                        Annee = voiture.Annee,
                        Couleur = voiture.Couleur,
                        Image_Name = voiture.Image_Name,
                        Prix = voiture.Prix,
                        Promotion = voiture.Promotion,
                        Places = voiture.Places,
                        Portes = voiture.Portes,
                        Carburant = voiture.Carburant,
                        Boite_Vitesse = voiture.Boite_Vitesse,
                        DateRetour = temp.Date_Retour,
                        DateReservation = temp.Date_Reservation
                    }).OrderBy(a => a.Id).ToPagedList(pageNumber ?? 1, PageSize);
        }

        public IPagedList<VoitureModel> FilteredVoitures(VoitureSearchFilter voitureSearchFilter)
        {
            var sqlWhere = PredicateBuilder.New<VoitureModel>(true);

            if(voitureSearchFilter.Marque != null)
                sqlWhere.And(voiture => voiture.Marque == voitureSearchFilter.Marque);

            if(voitureSearchFilter.location != null)
                sqlWhere.And(voiture => voiture.EmplacementPrise == voitureSearchFilter.location);

            if(voitureSearchFilter.Passagers != null)
            {
                try
                {
                    int Passagers = int.Parse(voitureSearchFilter.Passagers);
                    sqlWhere.And(voiture => voiture.Places == Passagers);
                }
                catch (Exception e)
                {
                    sqlWhere.And(voiture => voiture.Places > 5);
                }
                
            }

            if(voitureSearchFilter.Carburant != null)
                sqlWhere.And(voiture => voiture.Carburant == voitureSearchFilter.Carburant);

            if (voitureSearchFilter.Boite_Vitesse != null)
                sqlWhere.And(voiture => voiture.Boite_Vitesse == voitureSearchFilter.Boite_Vitesse);

            if (voitureSearchFilter.Portes != null)
                sqlWhere.And(voiture => voiture.Portes == voitureSearchFilter.Portes);

            if (voitureSearchFilter.Date != null)
                sqlWhere.And(voiture => voitureSearchFilter.Date < voiture.DateReservation || voitureSearchFilter.Date > voiture.DateRetour 
                || voiture.DateReservation == null  );



            var sqlQuery = (from voiture in voitureDB.Voiture
                            join reservation in voitureDB.Reservation
                            on voiture.Id_Voiture equals reservation.Id_Voit
                            into tempTable
                            from temp in tempTable.DefaultIfEmpty()
                            select new VoitureModel()
                            {
                                Id = voiture.Id_Voiture,
                                Marque = voiture.Marque,
                                Annee = voiture.Annee,
                                Couleur = voiture.Couleur,
                                Image_Name = voiture.Image_Name,
                                Prix = voiture.Prix,
                                Promotion = voiture.Promotion,
                                Places = voiture.Places,
                                Portes = voiture.Portes,
                                Carburant = voiture.Carburant,
                                Boite_Vitesse = voiture.Boite_Vitesse,
                                DateRetour = temp.Date_Retour,
                                DateReservation = temp.Date_Reservation,
                                EmplacementPrise = voiture.Emplacement_Prise
                            }).Where(sqlWhere);


            if(voitureSearchFilter.Prix != null)
            {
                if(voitureSearchFilter.Prix.Equals("Prix ascendant"))
                {
                    return sqlQuery.OrderByDescending(voiture => voiture.Prix).ToPagedList(voitureSearchFilter.page, PageSize);

                }else if(voitureSearchFilter.Prix.Equals("Prix descendant"))
                {
                    return sqlQuery.OrderBy(voiture => voiture.Prix).ToPagedList(voitureSearchFilter.page, PageSize);
                }
            }

            return sqlQuery.OrderBy(voiture => voiture.Id).ToPagedList(voitureSearchFilter.page, PageSize);

        }

        public Dictionary<string, List<string>> getFilterDropDownItems()
        {
            Dictionary<string, List<string>> valuePairs = new Dictionary<string, List<string>>();

            List<string> carburantItems = (from voiture in voitureDB.Voiture
                              select voiture.Carburant).Distinct().ToList();
            List<string> marqueItems = (from voiture in voitureDB.Voiture
                                           select voiture.Marque).Distinct().ToList();
            List<string> boiteVitesseItems = (from voiture in voitureDB.Voiture
                                        select voiture.Boite_Vitesse).Distinct().ToList();

            List<string> prix = new List<string>() { "Prix ascendant", "Prix descendant" };

            valuePairs.Add("carburant", carburantItems);
            valuePairs.Add("marque", marqueItems);
            valuePairs.Add("boiteVitesse", boiteVitesseItems);
            valuePairs.Add("prix", prix);
            return valuePairs;
        }

        public List<VoitureModel> GetCarsWithPromotion()
        {
            return (from voiture in voitureDB.Voiture
                    join reservation in voitureDB.Reservation
                    on voiture.Id_Voiture equals reservation.Id_Voit
                    into tempTable
                    from temp in tempTable.DefaultIfEmpty()
                    where voiture.Promotion != null && 
                    (temp.Id_Voit == null || temp.Date_Reservation > DateTime.Now || temp.Date_Retour < DateTime.Now) 
                    select new VoitureModel()
                    {
                        Id = voiture.Id_Voiture,
                        Marque = voiture.Marque,
                        Annee = voiture.Annee,
                        Couleur = voiture.Couleur,
                        Image_Name = voiture.Image_Name,
                        Prix = voiture.Prix,
                        Promotion = voiture.Promotion,
                        Places = voiture.Places,
                        Portes = voiture.Portes,
                        Carburant = voiture.Carburant,
                        Boite_Vitesse = voiture.Boite_Vitesse,
                        DateRetour = temp.Date_Retour,
                        DateReservation = temp.Date_Reservation
                    }).ToList();

        }

        public List<VoitureModel> GetMostUsedCars()
        {
            var orderedItemsQuery = (from reservation in voitureDB.Reservation
                                    group reservation by new { VoitureId = reservation.Id_Voit,
                                                               DateRetour = reservation.Date_Retour,
                                                               DateReservation = reservation.Date_Reservation}
                                    into reservationGroup
                                    let count = reservationGroup.Count()
                                    orderby count descending
                                    select new
                                    { VoitureId = reservationGroup.Key.VoitureId,
                                      DateRetour = reservationGroup.Key.DateRetour,
                                      DateReservation = reservationGroup.Key.DateReservation}).Take(8);


            return (from voiture in voitureDB.Voiture
                    join orderedItem in orderedItemsQuery
                    on voiture.Id_Voiture equals orderedItem.VoitureId
                    select new VoitureModel()
                    {
                        Id = voiture.Id_Voiture,
                        Marque = voiture.Marque,
                        Annee = voiture.Annee,
                        Couleur = voiture.Couleur,
                        Image_Name = voiture.Image_Name,
                        Prix = voiture.Prix,
                        Promotion = voiture.Promotion,
                        Places = voiture.Places,
                        Portes = voiture.Portes,
                        Carburant = voiture.Carburant,
                        Boite_Vitesse = voiture.Boite_Vitesse,
                        DateRetour = orderedItem.DateRetour,
                        DateReservation = orderedItem.DateReservation
                    }).ToList();

        }

        /** <summary>
         *  La methode retourne un tuple qui contient 
         *  nombre de voitures, 
         *  nombre de marques,  
         *  nombre de reservations par mois,
         *  nombre de clients
         * </summary>
         * <returns>
         * Tuple
         * <br/>
         * Item1 = nombre de voitures<br/>
         * Item2 = nombre de marques<br/>
         * Item3 = nombre de reservations par mois<br/>
         * Item4 = nombre de clients
         * </returns>**/
        public Tuple<int, int, int,int> GetStatistics()
        {
            int nombreVoitures = voitureDB.Voiture.Count();
            int nombreMarques = (from voiture in voitureDB.Voiture
                                 select voiture.Marque).Distinct().Count();

            int nombreProprietaires = voitureDB.Proprietaire.Count();
            int nombreLocatairess = voitureDB.Locataire.Count();

            //TODO nombre clients

            int nombreReservationsParMois = (from reservation in voitureDB.Reservation
                                             where reservation.Date_Reservation.Month == DateTime.Now.Month &&
                                             reservation.Date_Reservation.Year == DateTime.Now.Year
                                             select reservation.Date_Reservation).Count();


            return new Tuple<int, int, int,int>(nombreVoitures, nombreMarques, nombreReservationsParMois,nombreLocatairess+nombreProprietaires);
                                      
        }


        /*Id = voiture.Id,
        Marque = voiture.Marque,
        Annee = voiture.Annee,
        Couleur = voiture.Couleur,
        image_Voiture = voiture.image_Voiture,
        Id_Proprietaire = voiture.Id_Proprietaire,
        Prix = voiture.Prix,
        Promotion = voiture.Promotion,
        Places = voiture.Places,
        Portes = voiture.Portes,
        Carburant = voiture.Carburant,
        Boite_Vitesse = voiture.Boite_Vitesse,
        DateRetour = reservation.Date_retour,
        DateReservation = reservation.Date_retour,
        Proprietaire = voiture.Proprietaire,*/
    }
}