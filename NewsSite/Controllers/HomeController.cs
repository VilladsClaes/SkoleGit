using System;
using NewsSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    public class HomeController : Controller
    {

      
        NewsSiteEntities db = new NewsSiteEntities();

        //Vis forside
        public ActionResult Index()
        {
            ViewBag.Title = "Forsiden";


            MyViewModel myViewModel = new MyViewModel();
            myViewModel.AllNews = db.NyhederTable.ToList();
            
            return View(myViewModel);
        }

     
        [ChildActionOnly]
        public ActionResult Footer()
        {
            Kontaktoplysninger myKontakt = new Kontaktoplysninger();
            myKontakt = db.Kontaktoplysninger.FirstOrDefault();



            return PartialView(myKontakt);

        }


    }
}


