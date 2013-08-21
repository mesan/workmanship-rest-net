using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using workmanship_rest_net.Models;
using workmanship_rest_net.Repositories;

namespace workmanship_rest_net.Controllers
{
    public class ProsjektController : ApiController
    {
        private readonly IProsjektRepository _prosjektRepository;
        private readonly IBrukerRepository _brukerRepository;

        public ProsjektController()
        {
            _prosjektRepository = new ProsjektRepository();
            _brukerRepository = new BrukerRepository();
        }

        public ProsjektController(IProsjektRepository prosjektRepository, IBrukerRepository brukerRepository)
        {
            _prosjektRepository = prosjektRepository;
            _brukerRepository = brukerRepository;
        }

        // GET api/prosjekter
        public HttpResponseMessage GetProsjekter()
        {
            var prosjekter = _prosjektRepository.GetAlle();

            return Request.CreateResponse(HttpStatusCode.OK, prosjekter);
        }

        // GET api/prosjekter/5
        public HttpResponseMessage GetProsjekt(int id)
        {
            var prosjekt = _prosjektRepository.Get(id);

            if (prosjekt == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, prosjekt);
        }

        // GET api/brukere/1/prosjekter/
        public HttpResponseMessage GetProsjektForBruker(int brukerId)
        {
            var bruker = _brukerRepository.Get(brukerId);

            if (bruker == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var prosjekter = _prosjektRepository.GetProsjekterForBruker(brukerId);

            return Request.CreateResponse(HttpStatusCode.OK, prosjekter);
        }

        // POST api/prosjekter
        public HttpResponseMessage Postprosjekt(Prosjekt prosjekt)
        {
            if (prosjekt != null)
            {
                bool suksess = _prosjektRepository.LeggTil(prosjekt);

                if (suksess)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, prosjekt);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = prosjekt.ProsjektNr }));
                    return response;
                }
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/prosjekter/5
        public HttpResponseMessage PutBruker(int id, Prosjekt prosjekt)
        {
            if (id == prosjekt.ProsjektNr)
            {
                bool suksess = _prosjektRepository.Oppdater(prosjekt);

                if (suksess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // DELETE api/prosjekter/5
        public HttpResponseMessage DeleteProsjekt(int id)
        {
            var prosjekt = _prosjektRepository.Get(id);

            if (prosjekt == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _prosjektRepository.Slett(prosjekt);

            return Request.CreateResponse(HttpStatusCode.OK, prosjekt);
        }
    }
}
