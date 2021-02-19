using Projet_Asp_Location_Voitures_2021.Areas.Voitures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projet_Asp_Location_Voitures_2021.Models
{
    public class IndexViewModel
    {
        public List<VoitureModel> promotionVoitures { get; set; }
        public List<VoitureModel> featuredVoitures { get; set; }
        public int NombreVoitures { get; set; }
        public int NombreMarques { get; set; }
        public int NombreReservationsParMois { get; set; }

        public int NombreClients { get; set; }

    }
}