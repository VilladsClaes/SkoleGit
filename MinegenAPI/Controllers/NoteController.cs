using MinegenAPI.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;

namespace MinegenAPI.Controllers
{
    public class NoteController : ApiController
    {

        PopopDatabaseEntities db = new PopopDatabaseEntities();


        // GET api/Note
        //http://localhost:1163/api/Note
        public List<NoteTabel> Get()
        {
            List<NoteTabel> mineNoters = new List<NoteTabel>();
            mineNoters = db.NoteTabel.ToList();

            return mineNoters;
        }

        // GET api/Note/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/Note/5
        public void Post([FromBody]NoteTabel Tove)
        {
            db.NoteTabel.Add(Tove);
            db.SaveChanges();
            return;

        }

        // PUT api/<controller>/5
        public void Put([FromBody]NoteTabel Rettelse)
        {

            db.NoteTabel.AddOrUpdate(Rettelse);       
            db.SaveChanges();
            return;

        }

        // DELETE api/Note/5
        public void Delete(int id)
        {
            NoteTabel Sletnoget = db.NoteTabel.Find(id);

            db.NoteTabel.Remove(Sletnoget);
            db.SaveChanges();
        }
    }
}