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
            myViewModel.AllVidsteDu = db.VidsteDuTable.ToList();


            return View(myViewModel);
        }

        //Søgeformular på Layout SoegNyhed 
        public ActionResult SoegNyhed()
        {
            return View();
        }
        [HttpPost] //der er posted noget fra søge-formular
        public ActionResult SoegNyhed(string Soegetxt)
        {
            //søg efter søgeord i modellen/tabellen joke
            List<NyhederTable> NyhedsResultat = new List<NyhederTable>();
            NyhedsResultat = db.NyhederTable.Where(j => j.NyhederOverskrift.Contains(Soegetxt) || j.NyhederTekst.Contains(Soegetxt)).ToList();

            return View(NyhedsResultat);
        }

        //Visning af den enkelte nyhed baseret på id fra tabellen
        public ActionResult EnkeltNyhed(int? Id)
        {
            //hvis der ikke et id med.
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            NyhederTable Nyhed = db.NyhederTable.Where(c => c.id == Id).FirstOrDefault();

            if (Nyhed == null)
            {
                return RedirectToAction("Index");
            }

            return View(Nyhed);


        }

        //Kontaktoplysninger i Footer (PARTIAL)
        [ChildActionOnly]
        public ActionResult Footer()
        {

            MyViewModel ModeltilKontaktoplysninger = new MyViewModel();
            ModeltilKontaktoplysninger.Kontaktoplysning = db.Kontaktoplysninger.FirstOrDefault();

                  
            return PartialView("Footer", ModeltilKontaktoplysninger);

        }

        //Overskrifter på liste (PARTIAL)

        [ChildActionOnly]
        public ActionResult Overskrifter()
        {
            MyViewModel myOverskrifter = new MyViewModel();
            myOverskrifter.AllNews = db.NyhederTable.ToList();


            return PartialView("Overskrifter", myOverskrifter);

        }



    }
}


