using Opgave_REST_CRUD_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Opgave_REST_CRUD_API.Controllers
{
    public class CitatAPIController : ApiController
    {
        //Tilføj en instans af entiteterne og tilføj biblioteket med models øverst
        //C:\Users\vill0281\source\repos\SkoleGit\Opgave REST CRUD API\Web.config 
        CitaterEntities db = new CitaterEntities();

        // GET api/CitatAPI - test ved at indsætte http://localhost:1033/api/CitatAPI
        public List<Citat> Get() //Brug GET-metoden på en liste
        {
            List<Citat> CitaterPaaListe = new List<Citat>();
            CitaterPaaListe = db.Citat.ToList(); //fyld ny instans af citat-modellen med indhold fra databasen

            return CitaterPaaListe;
        }

        // GET api/CitatAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/CitatAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT api/CitatAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/CitatAPI/5
        public void Delete(int id)
        {
        }
    }
}