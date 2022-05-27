using System;
using System.Collections.Generic;

namespace LocationVoitureApi.Models
{
    public partial class Agence
    {
        public Agence()
        {
            Employeurs = new HashSet<Employeur>();
        }

        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Adresse { get; set; }

        public virtual ICollection<Employeur> Employeurs { get; set; }
    }
}
