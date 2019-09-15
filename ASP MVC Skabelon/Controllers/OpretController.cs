using System;
using System.Collections.Generic;
//Entity Migration skal bruges til at få filnavnet ind i databasen
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Skabelon;

namespace ASP_MVC_Skabelon.Controllers
{


    public class OpretController : Controller
    {
      
        ASP_MVC_Skabelon.Models.ASP_MVC_SkabelonEntities db = new Models.ASP_MVC_SkabelonEntities();
        
        //Forsiden
        public ActionResult Index()
        {
            //Hent tabellen som en liste
            List<ASP_MVC_Skabelon.Models.OpretTable> Oprettelser = new List<Models.OpretTable>();
            //Fyld listen med databasens indhold
            Oprettelser = db.OpretTable.ToList();
            //Vis listen til viewet
            return View(Oprettelser);
        }

        //Detaljevisning
        public ActionResult Detalje(int? Id)
        {
            
            //Hent tabellen som en tabel
            ASP_MVC_Skabelon.Models.OpretTable EnkeltOprettelse = new Models.OpretTable();
            //Fyld tabellen med én ting
            EnkeltOprettelse = db.OpretTable.Where(c => c.ID == Id).FirstOrDefault();

            
            if (EnkeltOprettelse == null)
            {
                return View("Index");

            }
            //Vis den fundne i viewet
            return View(EnkeltOprettelse);


        }

        //Opret en ny        
        public ActionResult OpretNy()
        {
            ASP_MVC_Skabelon.Models.OpretTable NyOprettelse = new Models.OpretTable();
            return View(NyOprettelse);
        }
        [HttpPost]
        public ActionResult OpretNy(ASP_MVC_Skabelon.Models.OpretTable NyOprettelse, HttpPostedFileBase GrafikFil)
        {
            //Hvis modellen ikke er overholdt eller hvis der mangler et billede
            if (!ModelState.IsValid || GrafikFil == null)
            {
                ViewBag.Besked = "Noget gik galt,Prøv igen....";
                // retur til formularen - send den allerede- udfyldt med retur
                return View(NyOprettelse);
            }
            //formularen er korrekt udfyldt -data gemmes i Db og fil uploads i korrekt mappe i webprojekt

            // Håndtere filen (snup filnavn til Db + gem filen i img mappe)
            string GrafikfilensNavn = System.IO.Path.GetFileName(GrafikFil.FileName);
            string DerHvorBilledetSkalHen = System.IO.Path.Combine(Server.MapPath("/Content/grafik/Oprettelser"), GrafikfilensNavn);

            if (!Directory.Exists(Server.MapPath("/Content/grafik/Oprettelser")))
            {
                Directory.CreateDirectory(Server.MapPath("/Content/grafik/Oprettelser"));
            }

            // gem filens stinavn i filsystemet
            GrafikFil.SaveAs(DerHvorBilledetSkalHen);

            // gem filens filnavn i Db
            NyOprettelse.Billede = GrafikfilensNavn; // fx hest.jpg
            db.OpretTable.Add(NyOprettelse);
            db.SaveChanges();

            


            return RedirectToAction("Index", "Home");
        }
        public ActionResult Slet(int? Id)

        {
            //Hvis der ikke en Id med, sparke tilbage til index side.
            if (Id == null)
                return RedirectToAction("Index");


            //Hente oprettelse som måske skal slettes
            ASP_MVC_Skabelon.Models.OpretTable DenTingSomSkalSlettes = db.OpretTable.Find(Id);
            if (DenTingSomSkalSlettes == null) //hvis oprettelsen ikke matcher Id
                return RedirectToAction("Index");

            //Vis den i viewet
            return View(DenTingSomSkalSlettes);
        }
        [HttpPost]
        public ActionResult Slet(ASP_MVC_Skabelon.Models.OpretTable DenTingSomSkalSlettes)
        {
            //Find denne record baseret på ID
            DenTingSomSkalSlettes = db.OpretTable.Find(DenTingSomSkalSlettes.ID);            

            if (DenTingSomSkalSlettes == null)// hvis der er ikke record som matcher id 
                return RedirectToAction("Index");

            // slet fysisk fil/img
            string DerHvorBilledetLigger = System.IO.Path.Combine(Server.MapPath("~/Content/grafik/Oprettelser"), DenTingSomSkalSlettes.Billede);


            // hvis der img i den angivet sti
            if (System.IO.File.Exists(DerHvorBilledetLigger))
                System.IO.File.Delete(DerHvorBilledetLigger);// imgsti slettet


            // slet fra Db
            db.OpretTable.Remove(DenTingSomSkalSlettes);
            db.SaveChanges();

            return RedirectToAction("Index", "Opret");
        }
        public ActionResult Ret(int? Id)

        {
            //Hvis der ikke en Id med, sparke tilbage til index side.
            if (Id == null)
                return RedirectToAction("Index");


            //Hente oprettelse som måske skal Rettes, og send den med til viewet.
            ASP_MVC_Skabelon.Models.OpretTable DenTingSomSkalRettes = db.OpretTable.Find(Id);
            if (DenTingSomSkalRettes == null) //hvis der er ikke Cartoon som matcher Id
                return RedirectToAction("Index");


            return View(DenTingSomSkalRettes);
        }
        [HttpPost]
        public ActionResult Ret(ASP_MVC_Skabelon.Models.OpretTable DenTingSomSkalRettes, HttpPostedFileBase NyGrafikFil)
        {
            NyGrafikFil = Request.Files["NyGrafikFil"];
            //hvis det er ikke udfyldt korrekt
            if (!ModelState.IsValid)
            {
               
                return View(DenTingSomSkalRettes);// return til formularen. send den allrede udfyldt med retur,
            }
            if (NyGrafikFil != null)
            {
                string GrafikFilsSti = System.IO.Path.Combine(Server.MapPath("Content/grafik/Oprettelser"), DenTingSomSkalRettes.Billede);
                // imgsti slettet hvis der er en
                if (System.IO.File.Exists(GrafikFilsSti))
                    System.IO.File.Delete(GrafikFilsSti);
                //Erstat den sti det gamle billede havde med det nye
                string DenNyeGrafikFil = System.IO.Path.GetFileName(NyGrafikFil.FileName);
                GrafikFilsSti = System.IO.Path.Combine(Server.MapPath("Content/grafik/Oprettelser"), DenNyeGrafikFil);

                NyGrafikFil.SaveAs(GrafikFilsSti);//gemme selve filen
                DenTingSomSkalRettes.Billede = DenNyeGrafikFil; // gemme navnet i modellen -som næste step sends til Db
            }
            db.OpretTable.AddOrUpdate(DenTingSomSkalRettes);
            db.SaveChanges();

            
            return RedirectToAction("Index", "Opret");
        }
    }
}