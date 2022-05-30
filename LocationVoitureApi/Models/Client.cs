using System;
using System.Collections.Generic;

namespace LocationVoitureApi.Models
{
    public partial class Client
    {
        public Client()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Cin { get; set; }
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
