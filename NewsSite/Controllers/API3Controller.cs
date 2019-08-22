using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Opgave_REST_CRUD_API.Controllers
{
    public class API3Controller : ApiController
    {
        //Tilføj en instans af entiteterne og tilføj biblioteket med models øverst
        //C:\Users\vill0281\source\repos\SkoleGit\Opgave REST CRUD API\Web.config 
        NewsSiteEntities db = new NewsSiteEntities();

        // GET api/CitatAPI - test ved at indsætte http://localhost:1033/api/API3
        public List<VidsteDuTable> Get() //Brug GET-metoden på en liste
        {
            List<VidsteDuTable> CitaterPaaListe = new List<VidsteDuTable>();
            CitaterPaaListe = db.VidsteDuTable.ToList(); //fyld ny instans af citat-modellen med indhold fra databasen

            return CitaterPaaListe;
        }

        // GET api/CitatAPI?soegeord=er
        public List<VidsteDuTable> Get(string soegeord)
        {
            List<VidsteDuTable> CitaterPaaListe = new List<VidsteDuTable>();
            CitaterPaaListe = db.VidsteDuTable.Where(x => x.VidsteDuTekst.Contains(soegeord) || x.VidsteDuOverskrift.Contains(soegeord)).ToList(); //fyld ny instans af citat-modellen med indhold fra databasen

            return CitaterPaaListe;
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