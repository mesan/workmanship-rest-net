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
    public class BrukerController : ApiController
    {
        private readonly IBrukerRepository _brukerRepository;
        private readonly IProsjektRepository _prosjektRepository;

        public BrukerController()
        {
            _brukerRepository = new BrukerRepository();
            _prosjektRepository = new ProsjektRepository();
        }

        public BrukerController(IBrukerRepository brukerRepository, IProsjektRepository prosjektRepository)
        {
            _brukerRepository = brukerRepository;
            _prosjektRepository = prosjektRepository;
        }

        // GET api/brukere
        public HttpResponseMessage GetBrukere()
        {
            var brukere = _brukerRepository.GetAlle();

            return Request.CreateResponse(HttpStatusCode.OK, brukere);
        }

        // GET api/brukere/5
        public HttpResponseMessage GetBruker(int id)
        {
            var bruker = _brukerRepository.Get(id);

            if (bruker == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, bruker);
        }

        // GET api/prosjekter/1/brukere
        public HttpResponseMessage GetBrukerForProsjekt(int prosjektId)
        {
            var prosjekt = _prosjektRepository.Get(prosjektId);

            if (prosjekt == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var brukere = _brukerRepository.GetBrukereForProsjekt(prosjektId);

            return Request.CreateResponse(HttpStatusCode.OK, brukere);
        }

        // POST api/brukere
        public HttpResponseMessage PostBruker(Bruker bruker)
        {
            if (bruker != null)
            {
                bool suksess = _brukerRepository.LeggTil(bruker);

                if (suksess)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, bruker);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = bruker.AnsattNummer }));
                    return response;
                }
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/brukere/5
        public HttpResponseMessage PutBruker(int id, Bruker bruker)
        {
            if (id == bruker.AnsattNummer)
            {
                bool suksess = _brukerRepository.Oppdater(bruker);

                if (!suksess)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // DELETE api/brukere/5
        public HttpResponseMessage DeleteBruker(int id)
        {
            var bruker = _brukerRepository.Get(id);

            if (bruker == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _brukerRepository.Slett(bruker);

            return Request.CreateResponse(HttpStatusCode.OK, bruker);
        }
    }
}
