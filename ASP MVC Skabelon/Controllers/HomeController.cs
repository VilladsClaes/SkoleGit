using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_Skabelon.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Forsiden
            return View();
        }

        public ActionResult Api1()
        {
            //Hent data fra model med API og ikke med ADO
            return View();
        }


    }
}