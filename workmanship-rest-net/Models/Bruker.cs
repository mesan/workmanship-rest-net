using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace workmanship_rest_net.Models
{
    public class Bruker
    {
        public int AnsattNummer { get; set; }
        public string BrukerId { get; set; }
        public string FulltNavn { get; set; }
        public bool HarPlussBetalt { get; set; }
        public long KontoNummer { get; set; }
        public int Stillingsprosent { get; set; }
        public string EpostAdr { get; set; }

        //public virtual Collection<Prosjekt> Prosjekter { get; set; }

        public Bruker()
        {
            HarPlussBetalt = true;
            Stillingsprosent = 100;
        }
    }
}