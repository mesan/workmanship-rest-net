using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using workmanship_rest_net.Models;
using Should;
using workmanship_rest_net.Repositories;

namespace workmanship_rest_net.Tests
{
    [TestClass]
    public class BrukereControllerTest
    {
        private HttpServer _server;
        private const string _url = "http://localhost/api";

        [TestInitialize]
        public void Setup()
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            _server = new HttpServer(config);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _server.Dispose();
            _server = null;
        }

        [TestMethod]
        public void Get_utenId_skalReturnere_alleBrukere()
        {
            var alleBrukere = Data.Instance.Brukere.ToList();

            using (var client = new HttpClient(_server))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var request = new HttpRequestMessage(HttpMethod.Get, _url + "/brukere"))
                using (var response = client.SendAsync(request).Result)
                {
                    var brukere = response.Content.ReadAsAsync<IEnumerable<Bruker>>().Result;
                    brukere = brukere.OrderBy(bruker => bruker.AnsattNummer).ToList();

                    brukere.ToList().Count.ShouldEqual(alleBrukere.Count);
                }
            }
        }
    }
}
