using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace workmanship_rest_net.Models
{
    public class Prosjekt
    {
        public bool Intern { get; set; }
        public string ProsjektNavn { get; set; }
        public int ProsjektNr { get; set; }

        [JsonIgnore]
        public virtual Collection<Bruker> Brukere { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var p = (Prosjekt) obj;
            return Intern == p.Intern && ProsjektNavn == p.ProsjektNavn && ProsjektNr == p.ProsjektNr;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int i = 23;
                int hash = 17;
                hash *= i + Intern.GetHashCode();
                hash *= i + ProsjektNavn.GetHashCode();
                hash *= i + ProsjektNr.GetHashCode();

                return hash;
            }
        }
    }
}