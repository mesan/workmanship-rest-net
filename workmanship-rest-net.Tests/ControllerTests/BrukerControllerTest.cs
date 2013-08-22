using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using workmanship_rest_net.Models;
using Should;
using workmanship_rest_net.Repositories;
using workmanship_rest_net.Tests.ControllerTests;

namespace workmanship_rest_net.Tests
{
    [TestClass]
    public class BrukerControllerTest : BaseControllerTest
    {
        private BrukerRepository _repo;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            Start("51234");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
           Stopp(); 
        }

        [TestInitialize]
        public void Init()
        {
            _repo = new BrukerRepository();
        }

        [TestMethod]
        public void Get_utenId_skalReturnere_alleBrukere()
        {
            // Arrange
            var expected = _repo.GetAlle().ToList();

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, "api/brukere"))
            {
                // Assert
                var brukere = GetContent<IEnumerable<Bruker>>(response);
                brukere.ToList().Count.ShouldEqual(expected.Count);
            }
        }

        [TestMethod]
        public void GetBruker_naarBrukerMedIdEksisterer_skalReturnere_enBruker()
        {
            // Arrange
            int id = 1;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, "api/brukere/" + id))
            {
                //Assert
                var bruker = GetContent<Bruker>(response);
                bruker.AnsattNummer.ShouldEqual(id);
            }
        }
        
        [TestMethod]
        public void GetBruker_naarBrukerMedIdIkkeEksisterer_skalReturnere_HttpStatusNotFound()
        {
            // Arrange
            int id = -1;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, "api/brukere/" + id))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }
        }
        
        [TestMethod]
        public void GetBrukereForProsjekt_naarProsjektMedIdIkkeEksisterer_skalReturnere_HttpStatusNotFound()
        {
            // Arrange
            int id = -1;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, String.Format("api/brukere/{0}/prosjekter", id)))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }
        }

        [TestMethod]
        public void GetBrukereForProsjekt_naarProsjektMedIdEksisterer_skalReturnere_brukere()
        {
            // Arrange
            int id = 2;
            var expected = _repo.GetBrukereForProsjekt(id);

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, String.Format("api/prosjekter/{0}/brukere", id)))
            {
                // Assert
                var brukere = GetContent<IEnumerable<Bruker>>(response);
                brukere.ShouldEqual(expected);
            }
        }
        
        [TestMethod]
        public void PostBruker_naarBrukerHarRikitigFormat_skalReturnere_HttpStatusCreatedOgOpprettetBruker()
        {
            // Arrange
            var bruker = new Bruker
                         {
                             AnsattNummer = 4,
                             BrukerId = "bruker4",
                             EpostAdr = "bruker4@epost.no",
                             FulltNavn = "Bruker4",
                             KontoNummer = 123456789L
                         };

            // Act
            using (var response = SendHttpRequest(HttpMethod.Post, "api/brukere", bruker))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.Created);
                
                var opprettetBruker = GetContent<Bruker>(response);
                opprettetBruker.ShouldEqual(bruker);
            }
        }

        [TestMethod]
        public void PutBruker_naarBrukerMedIdIkkeFinnes_skalReturnere_HttpStatusNotFound()
        {
            // Arrange
            var bruker = new Bruker
            {
                AnsattNummer = -1,
                BrukerId = "bruker1",
                EpostAdr = "bruker1@epost.no",
                FulltNavn = "Nytt navn",
                KontoNummer = 123456789L
            };

            // Act
            using (var response = SendHttpRequest(HttpMethod.Put, "api/brukere/" + bruker.AnsattNummer, bruker))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }
        }

        [TestMethod]
        public void PutBruker_naarBrukerMedIdFinnes_skalReturnere_HttpStatusOk()
        {
            // Arrange
            var bruker = new Bruker
            {
                AnsattNummer = 1,
                BrukerId = "bruker1",
                EpostAdr = "bruker1@epost.no",
                FulltNavn = "Nytt navn",
                KontoNummer = 123456789L
            };

            // Act
            using (var response = SendHttpRequest(HttpMethod.Put, "api/brukere/" + bruker.AnsattNummer, bruker))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            }
        }

        [TestMethod]
        public void DeleteBruker_naarBrukerMedIdIkkeFinnes_skalReturnere_HttpStatusNotFound()
        {
            // Arrange
            int id = -9;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Delete, "api/brukere/" + id))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }
        }
        
        [TestMethod]
        public void DeleteBruker_naarBrukerMedIdFinnes_skalReturnere_HttpStatusOkOgSlettetBruker()
        {
            // Arrange
            int id = 3;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Delete, "api/brukere/" + id))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.OK);

                var bruker = GetContent<Bruker>(response);
                bruker.AnsattNummer.ShouldEqual(id);
            }
        }
    }
}
