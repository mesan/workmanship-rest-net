using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Should.Core.Exceptions;
using workmanship_rest_net.Models;

namespace workmanship_rest_net.Repositories
{
    public class BrukerRepository : IBrukerRepository
    {
        private readonly Data _dataSource;

        public BrukerRepository()
        {
            _dataSource = Data.Instance;
        }

        public BrukerRepository(Data dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Bruker> GetAlle()
        {
            return _dataSource.Brukere.AsEnumerable();
        }
        
        public Bruker Get(int id)
        {
            return _dataSource.Brukere.FirstOrDefault(p => p.AnsattNummer == id);
        }

        public bool LeggTil(Bruker bruker)
        {
            _dataSource.Brukere.Add(bruker);

            return true;
        }

        public bool Slett(Bruker bruker)
        {
            bool suksess = _dataSource.Brukere.Remove(bruker);

            if (suksess)
            {
                var brukerProsjekter = _dataSource.Prosjekter.Where(prosjekt => prosjekt.Brukere != null && prosjekt.Brukere.Contains(bruker));

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
            return _dataSource.Brukere.Where(bruker => bruker.Prosjekter.Any(prosjekt => prosjekt.ProsjektNr == prosjektId));
        } 
    }
}