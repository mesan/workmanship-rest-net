using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
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

        /// <summary>
        /// Henter alle brukere.
        /// </summary>
        [GET("api/brukere")]
        public HttpResponseMessage GetBrukere()
        {
            var brukere = _brukerRepository.GetAlle();

            return Request.CreateResponse(HttpStatusCode.OK, brukere);
        }

        /// <summary>
        /// Henter bruker med angitt AnsattNummer.
        /// </summary>
        /// <param name="id">AnsattNummer</param>
        [GET("api/brukere/{id}", RouteName = "GetBrukerMedId")]
        public HttpResponseMessage GetBruker(int id)
        {
            var bruker = _brukerRepository.Get(id);

            if (bruker == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, bruker);
        }

        /// <summary>
        /// Henter brukere for prosjekt med angitt ProsjektNr
        /// </summary>
        /// <param name="prosjektId">ProsjektNr</param>
        [GET("api/prosjekter/{prosjektId}/brukere")]
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

        /// <summary>
        /// Oppretter en ny bruker
        /// </summary>
        /// <param name="bruker">Bruker som skal opprettes</param>
        [POST("api/brukere")]
        public HttpResponseMessage PostBruker(Bruker bruker)
        {
            if (bruker != null)
            {
                bool suksess = _brukerRepository.LeggTil(bruker);

                if (suksess)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, bruker);
                    response.Headers.Location = new Uri(Url.Link("GetBrukerMedId", new { id = bruker.AnsattNummer }));
                    return response;
                }
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Oppdaterer en bruker
        /// </summary>
        /// <param name="id">AnsattNummer</param>
        /// <param name="bruker">Bruker som skal oppdateres</param>
        [PUT("api/brukere/{id}")]
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

        /// <summary>
        /// Sletter bruker med angitt AnsattNummer
        /// </summary>
        /// <param name="id">AnsattNummer</param>
        [DELETE("api/brukere/{id}")]
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
