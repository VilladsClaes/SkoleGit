using MVCcitater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCcitater.Controllers
{
    public class HomeController : Controller
    {
        //Instans af modellen som giver dagang til databasen....
        DbCitaterEntities Db = new DbCitaterEntities();



        // GET: Home
        public ActionResult Index()
        {


            // lav en ny tom liste (til Citater fra db)
            List < Citater > CitatListe= new List<Citater>();


            //fyld Citater i listen
            CitatListe =Db.Citater.ToList();


            return View(CitatListe);
        }
        public ActionResult VisCitat(int? Id)
        {
            //Hvis der ikke Id Tal så redircte vi til Index side.....
            if(Id==null)
            
                return RedirectToAction("Index");
            


            Citater UdvalgtCitat = new Citater();
            UdvalgtCitat = Db.Citater.Where(lol => lol.CitatID == Id).FirstOrDefault();


            //hvis der ikke er et citat som matcher, så spark tilbage til Index side 
            if (UdvalgtCitat == null)

                return RedirectToAction("Index");
            return View(UdvalgtCitat);
        }

    }   
   
}