using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    
    public class AdminController : Controller
    {
        //Forhindr adgang til siden hvis der ikke er en session
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["LoginBruger"] == null)
            {
                filterContext.Result = new RedirectResult("~/login/");
            }
        }

        NewsSiteEntities db = new NewsSiteEntities();

        // Vis OpretNyhed-siden
        public ActionResult Index()
        {
           

            MyViewModel viewModel = new MyViewModel();
            viewModel.AllNews = db.NyhederTable.ToList();



            return View(viewModel);
        }


       
        public ActionResult OpretNyhed()
        {
            NyhederTable NyNyhed = new NyhederTable();
            NyNyhed.NyhederDato = DateTime.Now;
            return View(NyNyhed);
        }

        //OPRET NYHED
        [HttpPost]
        public ActionResult OpretNyhed(NyhederTable NyNyhed, HttpPostedFileBase NyhederImage)
        {
          if(!ModelState.IsValid || NyhederImage == null)
            {
                ViewBag.Besked = "Noget gik galt, prøv igen...";
                return View(NyNyhed); // retur til formularen - send den allerede- udfyldt med retur
            }
          //formularen er korrekt udfyldt -data gemmes i Db og fil uploads i korrekt mappe i webprojekt

            // Håndtere filen (snup filnavn til Db + gem filen i img mappe)
            string ImgNavn = System.IO.Path.GetFileName(NyhederImage.FileName);
            string ImgSti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/Nyheder/"), ImgNavn);


            // gem filen
            NyhederImage.SaveAs(ImgSti);

           


            // gem i Db
            NyNyhed.NyhederImage = ImgNavn; // fx hest.jpg
            db.NyhederTable.Add(NyNyhed);
            db.SaveChanges();



            TempData["Besked"] = "Nyhed er oprettet";


            return RedirectToAction("Index","Admin");
        }

        


        //Vis SletNyhed-siden
        public ActionResult SletNyhed(int? id)

        {
            //Hvis der ikke en Id med, sparke tilbage til index side.
            if (id == null)
                return RedirectToAction("Index");


            //Hente noget som måske skal slettes, og send den med til viewet.
            NyhederTable SletNyheden = db.NyhederTable.Find(id);
            if(SletNyheden == null) //hvis der er ikke Cartoon som matcher Id
                return RedirectToAction("Index");


            return View(SletNyheden);
        }

        //SLET NYHED
        [HttpPost]
        public ActionResult SletNyheden(NyhederTable SletNyheden)
        {
            //Hente noget som måske skal slettes. og sende den til viewet...
            //det har kun en id . så resten skal slåes op med "find"
            SletNyheden = db.NyhederTable.Find(SletNyheden.id);
            

            if (SletNyheden == null)// hvis der er ikke noget som matcher id 
                return RedirectToAction("Index");

            // slet fysisk fil/img
            string ImgSti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/Nyheder/"),SletNyheden.NyhederImage);


            // hvis der img i den angivet sti
            if (System.IO.File.Exists(ImgSti))
                System.IO.File.Delete(ImgSti);// imgsti slettet


            // slet fra Db
            db.NyhederTable.Remove(SletNyheden);
            db.SaveChanges();

            TempData["Besked"] = "Nyheden er slettet";

            return RedirectToAction("Index","Admin");
        }

        //Vis Retnyhed-siden
        public ActionResult RetNyhed(int? Id)

        {
            //Hvis der ikke en Id med, sparke tilbage til index side.
            if (Id == null)
                return RedirectToAction("Index");


            //Hente noget som måske skal Rettes, og send den med til viewet.
            NyhederTable NyhedRettes = db.NyhederTable.Find(Id);
            if (NyhedRettes == null) //hvis der er ikke noget som matcher Id
                return RedirectToAction("Index");


            return View(NyhedRettes);
        }

        //RET NYHED
        [HttpPost]
        public ActionResult RetNyhed(NyhederTable NyhedRettes, HttpPostedFileBase NytPhoto)
        {
            NytPhoto = Request.Files["NytPhoto"];
            //hvis det er ikke udfyldt korrekt
            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "Der er noget galt, prøv igen.....";
                return View(NyhedRettes);// return til formularen. send den allrede udfyldt med retur,
            }
            if(NytPhoto != null)
            {
                string imgsti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/Nyheder/"), NyhedRettes.NyhederImage);

                if (System.IO.File.Exists(imgsti))
                    System.IO.File.Delete(imgsti);// imgsti slettet



                string ImgNavn = System.IO.Path.GetFileName(NytPhoto.FileName);
                imgsti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/Nyheder/"), ImgNavn);

                NytPhoto.SaveAs(imgsti);//gemme selve filen
                NyhedRettes.NyhederImage = ImgNavn; // gemme navnet i modellen -som næste step sends til Db
            }
            db.NyhederTable.AddOrUpdate(NyhedRettes);
            db.SaveChanges();

            TempData["besked"] = "Nyhed er Rettet";

            return RedirectToAction("Index", "Admin");
        }

    }
}