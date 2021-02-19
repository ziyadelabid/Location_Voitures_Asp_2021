using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projet_Asp_Location_Voitures_2021.Areas.Voitures.DTOs
{
    public class VoitureSearchFilter
    {
        public int page { get; set; } = 1;
        public string Prix { get; set; }
        public string Marque { get; set; }
        public string Carburant { get; set; }
        public string Boite_Vitesse { get; set; }
        public string location { get; set; }
        public DateTime? Date { get; set; }
        public string Passagers { get; set; }
        public int? Portes { get; set; }
    }
}