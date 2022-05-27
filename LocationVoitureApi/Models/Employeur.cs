using System;
using System.Collections.Generic;

namespace LocationVoitureApi.Models
{
    public partial class Employeur
    {
        public Employeur()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Photo { get; set; }
        public string? Password { get; set; }
        public int Idagence { get; set; }

        public virtual Agence IdagenceNavigation { get; set; } = null!;
        public virtual ICollection<Location> Locations { get; set; }
    }
}
