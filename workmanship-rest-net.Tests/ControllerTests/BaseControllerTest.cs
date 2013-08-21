using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.SelfHost;
using AttributeRouting.Web.Http.SelfHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using workmanship_rest_net.Controllers;

namespace workmanship_rest_net.Tests.ControllerTests
{
    public class BaseControllerTest
    {
        //private HttpServer _server;
        private static HttpSelfHostServer _server;
        private static HttpClient _client;
        private const string Url = "http://localhost:51234/";

        public static void Start()
        {
            /*var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            _server = new HttpServer(config);
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/

            var config = new HttpSelfHostConfiguration(Url);

            config.Routes.MapHttpAttributeRoutes(cfg =>
                                                 {
                                                     cfg.AddRoutesFromController<BrukerController>();
                                                     cfg.AddRoutesFromController<ProsjektController>();
                                                 });

            _server = new HttpSelfHostServer(config);
            _server.OpenAsync().Wait();

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void Stopp()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }

            if (_server != null)
            {
                _server.CloseAsync();
                _server.Dispose();
                _server = null; 
            } 
        }

        protected T GetContent<T>(HttpResponseMessage response)
        {
            return response.Content.ReadAsAsync<T>().Result;
        }

        protected HttpResponseMessage SendHttpRequest(HttpMethod method, String path)
        {
            using (var request = new HttpRequestMessage(method, Url + path))
            {
                return _client.SendAsync(request).Result;
            }
        }
 
        protected HttpResponseMessage SendHttpRequest(HttpMethod method, String path, Object obj)
        {
            if (method == HttpMethod.Post)
            {
                return _client.PostAsJsonAsync(Url + path, obj).Result;
            } 
            else if (method == HttpMethod.Put)
            {
                return _client.PutAsJsonAsync(Url + path, obj).Result;
            }

            return SendHttpRequest(method, path);
        }
    }
}
