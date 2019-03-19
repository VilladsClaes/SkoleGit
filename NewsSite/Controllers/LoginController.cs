using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    public class LoginController : Controller
    {
        private NewsSiteEntities db = new NewsSiteEntities();

        // GET: Login
        public ActionResult Index()
        {
            MyViewModel loginVM = new MyViewModel();

            Session.Remove("LoginBruger");
            return View(loginVM);
        }

        [HttpPost]
        public ActionResult Index(Logins Bruger, FormCollection MinformCollection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            Logins BrugerMatch = new Logins();
            BrugerMatch = db.Logins.Where(b => b.BrugerNavn == Bruger.BrugerNavn).FirstOrDefault();
            //Ovenstående kan også skrives sådan her med Lambda-expression
            /*
              bruger = (from U in db.Logins where U.BrugerNavn == BrugerMatch.BrugerNavn select U).FirstOrDefault(); //Find om der er et navn i databasen som svarer til det der blev tastet i html-formularen
             */


            //Der er ikke noget match. Brugermatch == null
            if (BrugerMatch == null)
            {
                Session.Remove("LoginBruger");
                ViewBag.Besked = "Der er ikke match mellem brugernavn og password";
                return View();
            }
            //hvis der var et match
            //tjek password op mod hashet
            string MyPassword = HashSalt.HashPassword(MinformCollection["Kodeord"], BrugerMatch.Salt);
            BrugerMatch = (from U in db.Logins where U.BrugerNavn == BrugerMatch.BrugerNavn && U.Kodeord == MyPassword select U).FirstOrDefault(); //Find om der er et navn i databasen som svarer til det der blev tastet i html-formularen

            Session["LoginBruger"] = BrugerMatch.BrugerNavn;
            Session["BrugerID"] = BrugerMatch.Id;

            return RedirectToAction("Index", "Admin");
        }

        
        // GET: Logins/Details/5
        public ActionResult Details()
        {
            MyViewModel brugerprofil = new MyViewModel();
            brugerprofil.Logins = db.Logins.Find((int)Session["BrugerID"]);
            if (brugerprofil.Logins == null)
            {
                return HttpNotFound();
            }
            return View(brugerprofil);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Logins logins)
        {
            if (ModelState.IsValid)
            {

                string MySalt = HashSalt.GetRandomSalt(); //tager metode fra cs-filen som vi har lavet

                logins.Salt = MySalt; //Lægger salt'en ind i databasen for brugeren.
                logins.Kodeord = HashSalt.HashPassword(logins.Kodeord, MySalt); //tager metode fra cs-filen med to argumenter

                db.Logins.Add(logins);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }

            return View(logins);
        }

        // GET: Logins/Edit/5
        public ActionResult Edit()
        {
            MyViewModel editbruger = new MyViewModel();
            editbruger.Logins = db.Logins.Find((int)Session["BrugerID"]);
            if (editbruger.Logins == null)
            {
                return HttpNotFound();
            }

            return View(editbruger);
        }

        // POST: Logins/Edit/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MyViewModel editbruger)
        {
            if (ModelState.IsValid)
            {
                string NytSalt = HashSalt.GetRandomSalt(); //tager metode fra cs-filen som vi har lavet

                editbruger.Logins.Salt = NytSalt; //Lægger salt'en ind i databasen for brugeren.
                editbruger.Logins.Kodeord = HashSalt.HashPassword(editbruger.Logins.Kodeord, NytSalt); //tager metode fra cs-filen med to argumenter

                db.Entry(editbruger.Logins).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(editbruger.Logins);
        }

        // GET: Logins/Delete/5
        public ActionResult Delete()
        {

            MyViewModel sletbruger = new MyViewModel();
            sletbruger.Logins = db.Logins.Find((int)Session["BrugerID"]);
            if (sletbruger.Logins == null)
            {
                return HttpNotFound();
            }
            return View(sletbruger);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            MyViewModel sletbruger = new MyViewModel();
            sletbruger.Logins = db.Logins.Find((int)Session["BrugerID"]);
            db.Logins.Remove(sletbruger.Logins);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}