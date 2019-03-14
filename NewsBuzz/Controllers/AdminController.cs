using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsBuzz.Controllers
{
    public class AdminController : Controller
    {
        //Adminforsiden
        public ActionResult Index()
        {
            

            
            return View();
        }

        //En logud-metode
        public ActionResult Logud()
        {
            //Fjern alle sessioner
            Session.RemoveAll();
            //Går tilbage til det view vi kalder Index (over dette actionresult) i den controller der hedder Admin (den her controller)
            return RedirectToAction("Index", "Home");
        }
    }
}