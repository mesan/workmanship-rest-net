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
    public class BrukereController : ApiController
    {
        private readonly BrukereRepository _repository;

        public BrukereController()
        {
            _repository = new BrukereRepository();
        }

        public BrukereController(BrukereRepository repository)
        {
            _repository = repository;
        }

        // GET api/brukere
        public HttpResponseMessage GetBrukere()
        {
            var brukere = _repository.GetAlle();

            return Request.CreateResponse(HttpStatusCode.OK, brukere);
        }

        // GET api/brukere/5
        public HttpResponseMessage GetBruker(int id)
        {
            var bruker = _repository.Get(id);

            if (bruker == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, bruker);
        }

        // GET api/prosjekter/1/brukere
        public HttpResponseMessage GetBrukerForProsjekt(int prosjektId)
        {
            // TODO: Sjekke om prosjekt eksisterer

            /*if (prosjekt == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }*/

            var brukere = _repository.GetBrukereForProsjekt(prosjektId);

            return Request.CreateResponse(HttpStatusCode.OK, brukere);
        }

        // POST api/brukere
        public HttpResponseMessage PostBruker(Bruker bruker)
        {
            if (bruker != null)
            {
                bool suksess = _repository.LeggTil(bruker);

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
                bool suksess = _repository.Oppdater(bruker);

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

        // DELETE api/brukere/5
        public HttpResponseMessage DeleteBruker(int id)
        {
            var bruker = _repository.Get(id);

            if (bruker == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _repository.Slett(bruker);

            return Request.CreateResponse(HttpStatusCode.OK, bruker);
        }
    }
}
