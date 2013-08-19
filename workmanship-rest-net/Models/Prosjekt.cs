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
    }
}