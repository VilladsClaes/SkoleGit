using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hyponet.Controllers
{
    public class NoderAPIController : ApiController
    {
        // GET api/NoderAPI
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/NoderAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/NoderAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT api/NoderAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/NoderAPI/5
        public void Delete(int id)
        {
        }
    }
}