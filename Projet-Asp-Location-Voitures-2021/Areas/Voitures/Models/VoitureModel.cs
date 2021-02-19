using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projet_Asp_Location_Voitures_2021.Areas.Voitures.Models
{
    public class VoitureModel
    {
        public int Id { get; set; }
        public string Marque { get; set; }
        public string Couleur { get; set; }
        public Nullable<int> Annee { get; set; }
        public string Image_Voiture { get; set; }
        public string Image_Name { get; set; }
        public int Id_Proprietaire { get; set; }
        public decimal Prix { get; set; }
        public Nullable<decimal> Promotion { get; set; }
        public short Places { get; set; }
        public short Portes { get; set; }
        public string Carburant { get; set; }
        public string Boite_Vitesse { get; set; }

        public string EmplacementRetour { get; set; }
        public string EmplacementPrise { get; set; }

        public Nullable<DateTime>  DateRetour { get; set; }
        public Nullable<DateTime> DateReservation { get; set; }

        public string NomProprietaire { get; set; }

        


    }
}