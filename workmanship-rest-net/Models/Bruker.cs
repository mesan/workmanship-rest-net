using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual Collection<Prosjekt> Prosjekter { get; set; }

        public Bruker()
        {
            HarPlussBetalt = true;
            Stillingsprosent = 100;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var b = (Bruker) obj;

            return AnsattNummer == b.AnsattNummer && BrukerId == b.BrukerId && FulltNavn == b.FulltNavn &&
                   HarPlussBetalt == b.HarPlussBetalt && KontoNummer == b.KontoNummer &&
                   Stillingsprosent == b.Stillingsprosent;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int i = 23;
                int hash = 17;
                hash *= i + AnsattNummer.GetHashCode();
                hash *= i + BrukerId.GetHashCode();
                hash *= i + FulltNavn.GetHashCode();
                hash *= i + HarPlussBetalt.GetHashCode();
                hash *= i + KontoNummer.GetHashCode();
                hash *= i + Stillingsprosent.GetHashCode();

                return hash;
            }
        }
    }
}