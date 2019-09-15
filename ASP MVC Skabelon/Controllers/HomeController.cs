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

     
    }
}