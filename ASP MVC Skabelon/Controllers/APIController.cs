//Brug af modellen
using ASP_MVC_Skabelon.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace MinegenAPI.Controllers
{
    public class OpretController : ApiController
    {
        //Hvad er der i databasen?
        ASP_MVC_SkabelonEntities db = new ASP_MVC_SkabelonEntities();


        // GET api/Product
        //http://localhost:1163/api/Note
        public List<OpretTable> Get()
        {
            List<OpretTable> mineOprettelser = new List<OpretTable>();
            mineOprettelser = db.OpretTable.ToList();

            return mineOprettelser;
        }

        // GET api/Opret/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/Opret/5
        public void Post([FromBody]OpretTable Opret)
        {
            db.OpretTable.Add(Opret);
            db.SaveChanges();
            return;

        }

        // PUT api/<controller>/5
        public void Put([FromBody]OpretTable Rettelse)
        {

            db.OpretTable.AddOrUpdate(Rettelse);       
            db.SaveChanges();
            return;

        }

        // DELETE api/Note/5
        public void Delete(int id)
        {
            OpretTable Sletnoget = db.OpretTable.Find(id);

            db.OpretTable.Remove(Sletnoget);
            db.SaveChanges();
        }
    }
}