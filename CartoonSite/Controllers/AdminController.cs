﻿using CartoonSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CartoonSite.Controllers
{
    
    public class AdminController : Controller
    {
        CartoonDbEntities1 Db = new CartoonDbEntities1();

        // GET: Admin
        public ActionResult Index()
        {
            List<CartoonTable> CartoonList = new List<CartoonTable>();
            CartoonList = Db.CartoonTable.ToList();
            return View(CartoonList);
        }
        public ActionResult OpretNyCartoon()
        {
            CartoonTable NyCartoon = new CartoonTable();
            return View(NyCartoon);
        }
        [HttpPost]
        public ActionResult OpretNyCartoon(CartoonTable NyCartoon, HttpPostedFileBase Photo)
        {
          if(!ModelState.IsValid || Photo == null)
            {
                ViewBag.Besked = "Noget gik galt,Prøv igen....";
                return View(NyCartoon);// retur til formularen - send den allerede- udfyldt med retur
            }
          //formularen er korrekt udfyldt -data gemmes i Db og fil uploads i korrekt mappe i webprojekt

            // Håndtere filen (snup filnavn til Db + gem filen i img mappe)
            string ImgNavn = System.IO.Path.GetFileName(Photo.FileName);
            string ImgSti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/"), ImgNavn);


            // gem filen
            Photo.SaveAs(ImgSti);

            // gem i Db
            NyCartoon.Photo = ImgNavn; // fx hest.jpg
            Db.CartoonTable.Add(NyCartoon);
            Db.SaveChanges();



            TempData["Besked"] = "Cartoon er oprettet";


            return RedirectToAction("Index","Admin");
        }
        public ActionResult SletCartoon(int? Id)

        {
            //Hvis der ikke en Id med, sparke tilbage til index side.
            if (Id == null)
                return RedirectToAction("Index");


            //Hente Cartoon som måske skal slettes, og send den med til viewet.
            CartoonTable CartoonSlettes = Db.CartoonTable.Find(Id);
            if(CartoonSlettes == null) //hvis der er ikke Cartoon som matcher Id
                return RedirectToAction("Index");


            return View(CartoonSlettes);
        }
        [HttpPost]
        public ActionResult SletCartoon(CartoonTable CartoonSlettes)
        {
            //Hente Cartoon som måske skal slettes. og sende den til viewet...
            //Cartoon har kun en id . så resten skla slås op med "find"
            CartoonSlettes = Db.CartoonTable.Find(CartoonSlettes.CartoonID);


            //CartoonTable SC = Db.CartoonTable.Find(Id);

            if (CartoonSlettes == null)// hvis der er ikke Cartoon som matcher id 
                return RedirectToAction("Index");

            // slet fysisk fil/img
            string ImgSti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/"),CartoonSlettes.Photo);


            // hvis der img i den angivet sti
            if (System.IO.File.Exists(ImgSti))
                System.IO.File.Delete(ImgSti);// imgsti slettet


            // slet fra Db
            Db.CartoonTable.Remove(CartoonSlettes);
            Db.SaveChanges();

            TempData["Besked"] = "Cartoon er slettet";
            return RedirectToAction("Index","Admin");
        }
        public ActionResult RetCartoon(int? Id)

        {
            //Hvis der ikke en Id med, sparke tilbage til index side.
            if (Id == null)
                return RedirectToAction("Index");


            //Hente Cartoon som måske skal Rettes, og send den med til viewet.
            CartoonTable CartoonRettet = Db.CartoonTable.Find(Id);
            if (CartoonRettet == null) //hvis der er ikke Cartoon som matcher Id
                return RedirectToAction("Index");


            return View(CartoonRettet);
        }
        [HttpPost]
        public ActionResult RetCartoon(CartoonTable CartoonRettet, HttpPostedFileBase NytPhoto)
        {
            NytPhoto = Request.Files["NytPhoto"];
            //hvis det er ikke udfyldt korrekt
            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "Der er noget galt,Prøve igen.....";
                return View(CartoonRettet);// return til formularen. send den allrede udfyldt med retur,
            }
            if(NytPhoto != null)
            {
                string imgsti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/"), CartoonRettet.Photo);

                if (System.IO.File.Exists(imgsti))
                    System.IO.File.Delete(imgsti);// imgsti slettet



                string ImgNavn = System.IO.Path.GetFileName(NytPhoto.FileName);
                imgsti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/"), ImgNavn);

                NytPhoto.SaveAs(imgsti);//gemme selve filen
                CartoonRettet.Photo = ImgNavn; // gemme navnet i modellen -som næste step sends til Db
            }
            Db.CartoonTable.AddOrUpdate(CartoonRettet);
            Db.SaveChanges();

            TempData["besked"] = "Cartoon er Rettet";
            return RedirectToAction("Index", "Admin");
        }

    }
}