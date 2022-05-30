using System;
using System.Collections.Generic;

namespace LocationVoitureApi.Models
{
    public partial class Voiture
    {
        public Voiture()
        {
            Locations = new HashSet<Location>();
        }

        public string Matricule { get; set; } = null!;
        public int? Poids { get; set; }
        public decimal? Prix { get; set; }
        public string? Modele { get; set; }
        public string? Type { get; set; }
        public string? Photo { get; set; }
        public int Idmarque { get; set; }

        public virtual Marque marque { get; set; } = null!;
        public virtual ICollection<Location> Locations { get; set; }
    }
}
