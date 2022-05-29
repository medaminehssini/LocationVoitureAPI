using System;
using System.Collections.Generic;

namespace LocationVoitureApi.Models
{
    public partial class Agence
    {
        public Agence()
        {
            Employeurs = new HashSet<Employer>();
        }

        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Adresse { get; set; }

        public virtual ICollection<Employer> Employeurs { get; set; }
    }
}
