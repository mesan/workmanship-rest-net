﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using workmanship_rest_net.Models;

namespace workmanship_rest_net.Repositories
{
    public class ProsjektRepository : IProsjektRepository
    {
        private readonly Data _dataSource;

        public ProsjektRepository()
        {
            _dataSource = Data.Instance;
        }

        public ProsjektRepository(Data dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Prosjekt> GetAlle()
        {
            return _dataSource.Prosjekter.AsEnumerable();
        }

        public Prosjekt Get(int id)
        {
            return _dataSource.Prosjekter.FirstOrDefault(prosjekt => prosjekt.ProsjektNr == id);
        }

        public bool LeggTil(Prosjekt prosjekt)
        {
            _dataSource.Prosjekter.Add(prosjekt);

            return true;
        }

        public bool Oppdater(Prosjekt prosjekt)
        {
            Prosjekt p = Get(prosjekt.ProsjektNr);

            if (p != null)
            {
                p.Intern = prosjekt.Intern;
                p.ProsjektNavn = prosjekt.ProsjektNavn;
                p.Brukere = prosjekt.Brukere;

                return true;
            }

            return false;
        }

        public bool Slett(Prosjekt prosjekt)
        {
            bool suksess = _dataSource.Prosjekter.Remove(prosjekt);

            if (suksess)
            {
                var prosjektBrukere = _dataSource.Brukere.Where(bruker => (bruker.Prosjekter != null && bruker.Prosjekter.Contains(prosjekt)));

                foreach (var bruker in prosjektBrukere)
                {
                    bruker.Prosjekter.Remove(prosjekt);
                }
            }

            return suksess;
        }

        public IEnumerable<Prosjekt> GetProsjekterForBruker(int brukerId)
        {
            return _dataSource.Prosjekter.Where(prosjekt => prosjekt.Brukere.Any(bruker => bruker.AnsattNummer == brukerId));
        } 
    }
}