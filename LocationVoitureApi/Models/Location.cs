using System;
using System.Collections.Generic;

namespace LocationVoitureApi.Models
{
    public partial class Location
    {
        public int Id { get; set; }
        public DateTime? DateDeb { get; set; }
        public DateTime? DateFin { get; set; }
        public int IdClient { get; set; }
        public string VoitureMatricule { get; set; } = null!;
        public int Idemployeur { get; set; }

        public virtual Client IdClientNavigation { get; set; } = null!;
        public virtual Employeur IdemployeurNavigation { get; set; } = null!;
        public virtual Voiture VoitureMatriculeNavigation { get; set; } = null!;
    }
}
