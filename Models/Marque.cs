using System;
using System.Collections.Generic;

namespace LocationVoitureApi.Models
{
    public partial class Marque
    {
        public Marque()
        {
            Voitures = new HashSet<Voiture>();
        }

        public int Id { get; set; }
        public string? Nom { get; set; }

        public virtual ICollection<Voiture> Voitures { get; set; }
    }
}
