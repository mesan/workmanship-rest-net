using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.SelfHost;
using AttributeRouting.Web.Http.SelfHost;
using workmanship_rest_net.Controllers;

namespace workmanship_rest_net.Tests.ControllerTests
{
    public class BaseControllerTest
    {
        private static HttpSelfHostServer _server;
        private static HttpClient _client;
        private static string _url;

        public static void Start(String port)
        {
            _url = String.Format("http://localhost:{0}/", port);
            var config = new HttpSelfHostConfiguration(_url);

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
                _server.CloseAsync().Wait();
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
            using (var request = new HttpRequestMessage(method, _url + path))
            {
                return _client.SendAsync(request).Result;
            }
        }
 
        protected HttpResponseMessage SendHttpRequest(HttpMethod method, String path, Object obj)
        {
            if (method == HttpMethod.Post)
            {
                return _client.PostAsJsonAsync(_url + path, obj).Result;
            } 
            else if (method == HttpMethod.Put)
            {
                return _client.PutAsJsonAsync(_url + path, obj).Result;
            }

            return SendHttpRequest(method, path);
        }
    }
}
