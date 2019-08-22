using System;
using System.Collections.Generic;
//Husk at bruge Entity for at kunne bruge databaser
using System.Data.Entity;
using System.Linq;
//Husk at bruge .net for at bruge http
using System.Net;
//For at kunne sende med SMTP skal dette bibliotek tilkobles
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
//Husk at tilføje modellerne
using Ditmit.Models;

namespace Ditmit.Controllers
{
    public class HomeController : Controller
    {


        
        //Opret en instans af databasen.
        Ditmit.Models.dbkg218_13Entities1 db = new dbkg218_13Entities1();
       
     
        //Vis forsiden
        public ActionResult Index()
        {
            
            return View();
        }


        //Den model som vi bruger på forsiden @model Ditmit.Models.HelpViewModel kobles til formularens input name="helpOffer.HelpOfferText" id="helpOffer.HelpOfferText" og  label for="helpOffer.HelpOfferText" 
        //Value postes via formularen method="post" og knappen type="submit" til controllerens  [httppost] med samme ActionResult-navn som View'ets formular action="/Home/AddHelpOffer" 
        [HttpPost]
        public ActionResult AddHelpOffer(HelpViewModel helpvm)
        {

            //Hvis den tomme model udfyldes forkert
            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "noget gik galt";
                return View();
            }

            //Inden brugeren submitter skal controlleren sende DateTime med til db
            helpvm.Helpitem.HelpDate = DateTime.Today;
            //send hjælpetypen med. 1 = Hjælpebehov og 2 = Hjælpetilbud
            helpvm.Helpitem.HelpType_FK = 2;

            //Den udfyldte model tilføjes instansen af databasen
            db.Help.Add(helpvm.Helpitem);
            //Databasen Gemmes 
            db.SaveChanges();
            //Brugeren sendes videre (til samme view)
            return RedirectToAction("Index", "Home");
        }


        //Det samme som ovenfor, for Formularen med Actionresult AddHelpWant
        [HttpPost]
        public ActionResult AddHelpWant(HelpViewModel helpvm)
        {

            
            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "noget gik galt";
                return View();
            }

            helpvm.Helpitem.HelpDate = DateTime.Today;
            helpvm.Helpitem.HelpType_FK = 1;

            db.Help.Add(helpvm.Helpitem);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        //Vis et Partial View
        public ActionResult AltingTabel()
        {
            //instantier viewmodellen
            HelpViewModel vm = new HelpViewModel();
            //bind klasserne i viewmodellen sammen med databasen
            vm.helpitems = db.Help.ToList();
            //vis viewmodellen
            return View(vm);
        }





        //SØGEFORMULAR
        //public ActionResult Search()
        //{
        //    return View();
        //}
        //Lav det samme actionresult som håndterer det httppost som formularen sender
        [HttpPost]
        public ActionResult Search(string searchstring)
        {
            HelpViewModel searchresults = new HelpViewModel();
            //bind klasserne i viewmodellen sammen med databasen
      
            searchresults.helpitems = db.Help.ToList();
            
            searchresults.helpitems = db.Help.Where(g => g.HelpText.Contains(searchstring)).ToList();
            return View(searchresults);
        }
        //SØGEFORMULAR SLUT



    }
}