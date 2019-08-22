using NewsSite.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace NewsSite.Controllers
{
    public class API1Controller : ApiController
    {

        NewsSiteEntities db = new NewsSiteEntities();


        // GET api/Note
        //http://localhost:1163/api/Note
        public List<VidsteDuTable> Get()
        {
            List<VidsteDuTable> mineNoters = new List<VidsteDuTable>();
            mineNoters = db.VidsteDuTable.ToList();

            return mineNoters;
        }

        // GET api/Note/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/Note/5
        public void Post([FromBody]VidsteDuTable Tove)
        {
            db.VidsteDuTable.Add(Tove);
            db.SaveChanges();
            return;

        }

        // PUT api/<controller>/5
        public void Put([FromBody]VidsteDuTable Rettelse)
        {

            db.VidsteDuTable.AddOrUpdate(Rettelse);       
            db.SaveChanges();
            return;

        }

        // DELETE api/Note/5
        public void Delete(int id)
        {
            VidsteDuTable Sletnoget = db.VidsteDuTable.Find(id);

            db.VidsteDuTable.Remove(Sletnoget);
            db.SaveChanges();
        }
    }
}