using System;
using System.Collections.ObjectModel;
using workmanship_rest_net.Models;

namespace workmanship_rest_net.Repositories
{
    public class Data
    {
        private static readonly Lazy<Data> _instance = new Lazy<Data>(() => new Data());
        private readonly Object _objectLock = new object();

        private readonly Collection<Bruker> _brukere;
        private readonly Collection<Prosjekt> _prosjekter;

        public Collection<Bruker> Brukere
        {
            get
            {
                lock (_objectLock)
                {
                    return _brukere;
                }
            }
        }

        public Collection<Prosjekt> Prosjekter
        {
            get
            {
                lock (_objectLock)
                {
                    return _prosjekter;
                }
            }
        }

        private Data()
        {
            var bruker1 = new Bruker
                          {
                              AnsattNummer = 1,
                              BrukerId = "bruker1",
                              EpostAdr = "bruker1@epost.no",
                              FulltNavn = "Bruker1",
                              KontoNummer = 123456789L
                          };

            var bruker2 = new Bruker
                          {
                              AnsattNummer = 2,
                              BrukerId = "bruker2",
                              EpostAdr = "bruker2@epost.no",
                              FulltNavn = "Bruker2",
                              KontoNummer = 234567890L
                          };

            var bruker3 = new Bruker
                          {
                              AnsattNummer = 3,
                              BrukerId = "bruker3",
                              EpostAdr = "bruker3@epost.no",
                              FulltNavn = "Bruker3",
                              KontoNummer = 345678901L
                          };

            var prosjekt1 = new Prosjekt {Intern = true, ProsjektNr = 1, ProsjektNavn = "Intern Prosjekt 1"};
            var prosjekt2 = new Prosjekt {Intern = false, ProsjektNr = 2, ProsjektNavn = "Ekstern Prosjekt 1"};
            var prosjekt3 = new Prosjekt {Intern = false, ProsjektNr = 3, ProsjektNavn = "Ekstern Prosjekt 2"};

            /*bruker1.Prosjekter = new Collection<Prosjekt> {prosjekt1, prosjekt2};
            bruker2.Prosjekter = new Collection<Prosjekt> {prosjekt2};
            bruker3.Prosjekter = new Collection<Prosjekt>();*/

            prosjekt1.Brukere = new Collection<Bruker> {bruker1};
            prosjekt2.Brukere = new Collection<Bruker> {bruker1, bruker2};
            prosjekt3.Brukere = new Collection<Bruker>();

            _brukere = new Collection<Bruker> {bruker1, bruker2, bruker3};
            _prosjekter = new Collection<Prosjekt> {prosjekt1, prosjekt2, prosjekt3};
        }
    
        public static Data Instance
        {
           get { return _instance.Value; }
        }
    }
}