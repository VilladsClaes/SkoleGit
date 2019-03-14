using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LP_Butik_MVC_Repetetion.Controllers
{
    public class HomeController : Controller
    {

        LoginDatabaseEntities db = new LoginDatabaseEntities(); //Få fat i databasen

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Logins bruger, FormCollection MinformCollection)
        {

            bruger = (from U in db.Logins where U.BrugerNavn == bruger.BrugerNavn select U).FirstOrDefault(); //Find om der er et navn i databasen som svarer til det der blev tastet i html-formularen

            if (bruger != null)
            {
                //tjek password op mod hashet
                string MyPassword = HashSalt.HashPassword(MinformCollection["Kodeord"], bruger.Salt);
                bruger = (from U in db.Logins where U.BrugerNavn == bruger.BrugerNavn && U.Kodeord == MyPassword select U).FirstOrDefault(); //Find om der er et navn i databasen som svarer til det der blev tastet i html-formularen
                //Så er både bruger og kodeord rigtig
                if (bruger != null)
                {
                    Session["BrugerNavn"] = bruger.BrugerNavn;
                    Session["BrugerID"] = bruger.Id;
                    Response.Redirect("~/Logins/Index");
                }

            }


            return View();
        }

    }
}