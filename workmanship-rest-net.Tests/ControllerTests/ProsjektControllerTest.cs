using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using workmanship_rest_net.Models;
using workmanship_rest_net.Repositories;

namespace workmanship_rest_net.Tests.ControllerTests
{
    [TestClass]
    public class ProsjektControllerTest : BaseControllerTest
    {
        private ProsjektRepository _repo;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            Start("52345");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Stopp();
        }

        [TestInitialize]
        public void Init()
        {
            _repo = new ProsjektRepository();
        }

        [TestMethod]
        public void Get_utenId_skalReturnere_alleProsjekter()
        {
            // Arrange
            var expected = _repo.GetAlle().ToList();

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, "api/prosjekter"))
            {
                // Assert
                var prosjekter = GetContent<IEnumerable<Prosjekt>>(response);
                prosjekter.ToList().Count.ShouldEqual(expected.Count);
            }
        }

        [TestMethod]
        public void GetProsjekt_naarProsjektMedIdEksisterer_skalReturnere_etProsjekt()
        {
            // Arrange
            int id = 1;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, "api/prosjekter/" + id))
            {
                //Assert
                var prosjekt = GetContent<Prosjekt>(response);
                prosjekt.ProsjektNr.ShouldEqual(id);
            }
        }

        [TestMethod]
        public void GetProsjekt_naarProsjektMedIdIkkeEksisterer_skalReturnere_HttpStatusNotFound()
        {
            // Arrange
            int id = -1;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, "api/prosjekter/" + id))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }
        }

        [TestMethod]
        public void GetProsjekterForBruker_naarBrukerMedIdIkkeEksisterer_skalReturnere_HttpStatusNotFound()
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
        public void GetProsjekterForBruker_naarBrukerMedIdEksisterer_skalReturnere_prosjekter()
        {
            // Arrange
            int id = 2;
            var expected = _repo.GetProsjekterForBruker(id);

            // Act
            using (var response = SendHttpRequest(HttpMethod.Get, String.Format("api/brukere/{0}/prosjekter", id)))
            {
                // Assert
                var prosjekter = GetContent<IEnumerable<Prosjekt>>(response);
                prosjekter.ShouldEqual(expected);
            }
        }

        [TestMethod]
        public void PostProsjekt_naarProsjektHarRikitigFormat_skalReturnere_HttpStatusCreatedOgOpprettetProsjektr()
        {
            // Arrange
            var prosjekt = new Prosjekt
                           {
                               Intern = true,
                               ProsjektNavn = "ProsjektNavn5",
                               ProsjektNr = 5
                           };

            // Act
            using (var response = SendHttpRequest(HttpMethod.Post, "api/prosjekter", prosjekt))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.Created);

                var opprettetProsjekt = GetContent<Prosjekt>(response);
                opprettetProsjekt.ShouldEqual(prosjekt);
            }
        }

        [TestMethod]
        public void PutProsjekt_naarProsjektMedIdIkkeFinnes_skalReturnere_HttpStatusNotFound()
        {
            // Arrange
            var prosjekt = new Prosjekt
                           {
                               Intern = true,
                               ProsjektNavn = "ProsjektNavn",
                               ProsjektNr = -1
                           };

            // Act
            using (var response = SendHttpRequest(HttpMethod.Put, "api/prosjekter/" + prosjekt.ProsjektNr, prosjekt))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }
        }

        [TestMethod]
        public void PutProsjekt_naarProsjektMedIdFinnes_skalReturnere_HttpStatusOk()
        {
            // Arrange
            var prosjekt = new Prosjekt
                           {
                               Intern = true,
                               ProsjektNavn = "ProsjektNavn",
                               ProsjektNr = 1
                           };

            // Act
            using (var response = SendHttpRequest(HttpMethod.Put, "api/prosjekter/" + prosjekt.ProsjektNr, prosjekt))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            }
        }

        [TestMethod]
        public void DeleteProsjekt_naarProsjektMedIdIkkeFinnes_skalReturnere_HttpStatusNotFound()
        {
            // Arrange
            int id = -9;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Delete, "api/prosjekter/" + id))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }
        }

        [TestMethod]
        public void DeleteProsjektr_naarProsjektMedIdFinnes_skalReturnere_HttpStatusOkOgSlettetProsjekt()
        {
            // Arrange
            int id = 3;

            // Act
            using (var response = SendHttpRequest(HttpMethod.Delete, "api/prosjekter/" + id))
            {
                // Assert
                response.StatusCode.ShouldEqual(HttpStatusCode.OK);

                var prosjekt = GetContent<Prosjekt>(response);
                prosjekt.ProsjektNr.ShouldEqual(id);
            }
        }
    }
}
