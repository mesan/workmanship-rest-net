using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Should.Core.Exceptions;
using workmanship_rest_net.Models;

namespace workmanship_rest_net.Repositories
{
    public class BrukereRepository : IRepository<Bruker>
    {
        private readonly Data _datasource;

        public BrukereRepository()
        {
            _datasource = Data.Instance;
        }

        public BrukereRepository(Data datasource)
        {
            _datasource = datasource;
        }

        public IEnumerable<Bruker> GetAlle()
        {
            return _datasource.Brukere.AsEnumerable();
        }
        
        public Bruker Get(int id)
        {
            return _datasource.Brukere.FirstOrDefault(p => p.AnsattNummer == id);
        }

        public bool LeggTil(Bruker bruker)
        {
            _datasource.Brukere.Add(bruker);

            return true;
        }

        public bool Slett(Bruker bruker)
        {
            bool suksess = _datasource.Brukere.Remove(bruker);

            if (suksess)
            {
                var brukerProsjekter = _datasource.Prosjekter.Where(prosjekt => prosjekt.Brukere.Contains(bruker));

                foreach(var prosjekt in brukerProsjekter)
                {
                    prosjekt.Brukere.Remove(bruker);
                }
            }

            return suksess;
        }

        public bool Oppdater(Bruker bruker)
        {
            Bruker b = Get(bruker.AnsattNummer);
            
            if (b != null)
            {
                b.FulltNavn = bruker.FulltNavn;
                b.EpostAdr = bruker.EpostAdr;
                b.HarPlussBetalt = bruker.HarPlussBetalt;
                b.AnsattNummer = bruker.AnsattNummer;
                b.KontoNummer = bruker.KontoNummer;
                b.Stillingsprosent = bruker.Stillingsprosent;

                return true;
            }

            return false;
        }

        public IEnumerable<Bruker> GetBrukereForProsjekt(int prosjektId)
        {
            var prosjekt = _datasource.Prosjekter.FirstOrDefault(p => p.ProsjektNr == prosjektId);

            if (prosjekt != null)
            {
                return prosjekt.Brukere;
            }

            return new List<Bruker>().AsEnumerable();
        } 
    }
}