using MVCcitater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCcitater.Controllers
{
    public class LogIndController : Controller
    {
        // GET: LogInd



        //addgang til databasen
        DbCitaterEntities Db = new DbCitaterEntities();
        public ActionResult Index()
        {
            //slet en evt. brugersession når man lander på logind soíde- ekstra sikkerhed og t
            Session.Remove("BurgerSession");
            return View();
        }
        [HttpPost]
        public ActionResult Index(BrugerTable LogIndBruger)
        {
            //hvis modellen er udfyldt med fejl/mangler- send bruger tilbage til viwet ( her er samme side )
            if(!ModelState.IsValid)
                return View();
            // find ud af om der er et match mellem logind input og brugernavn/password i tablen
            BrugerTable BrugerMatch = new BrugerTable();
         BrugerMatch = Db.BrugerTable.Where(b=> b.BrugeNavn == LogIndBruger.BrugeNavn && b.PassWord == LogIndBruger.PassWord).FirstOrDefault();

            // brugermatch == null .....der var ikke noget match
            if(BrugerMatch == null)
            {

                // her er mere sikerhed for logind da vi sletter sission hvis der noget forkert.....
                Session.Remove("BurgerSession");


                ViewBag.Besked = "UPS! forkert brugernavn og/eller Password Prøv igen";
                return View(); // retun samme side med beskden.
            }


            // der blev fundet et match =login = session
            //der blev lavet en session - som er den vi "måler" på, om brugen skal have adgang til admin

            Session["BurgerSession"] = BrugerMatch.Navn;


            return RedirectToAction("Index","ADMIN"); // bruger sendes til startsiden på ADMIN
        }
    }
}