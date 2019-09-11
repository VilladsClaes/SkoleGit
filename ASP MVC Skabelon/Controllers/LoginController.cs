using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Skabelon.Models;

namespace ASP_MVC_Skabelon.Controllers
{
    public class LoginController : Controller
    {
        //Opret en udgave af Login-databasen kaldet db
        public ASP_MVC_Skabelon.Models.MVC_Skabelon_DatabaseEntities db = new Models.MVC_Skabelon_DatabaseEntities();

        public ActionResult Index()
        {
            //Login-forsiden viser en liste med indholdet af brugeroplysninger
            return View(db.LoginTabel.ToList());
        }

        
        public ActionResult Vis(int? id)
        {
            //Login-detaljevisningen viser en bruger baseret på det id der søges på
            ASP_MVC_Skabelon.Models.LoginTabel brugere = db.LoginTabel.Find(id);
           
            return View(brugere);
        }

        public ActionResult Opret()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Opret(LoginTabel Nybruger)
        {
            if (ModelState.IsValid)
            {
                //tager metode fra cs-filen som vi har lavet
                string UniktSalt = HashSalt.GetRandomSalt();
                //lægger denne string ind i tabellen
                Nybruger.Salt = UniktSalt;
                //tager brugerens password, kombinerer det med saltet, bruger SHA-algoritmen på det og lægger denne kode ind  i tabellen
                Nybruger.Password = HashSalt.HashPassword(Nybruger.Password, UniktSalt); 

                db.LoginTabel.Add(Nybruger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Nybruger);
        }

        public ActionResult Ret(int? id)
        {
            //slår en bruger op baseret på det ID der søges
            LoginTabel bruger = db.LoginTabel.Find(id);
            
            return View(bruger);
        }


        
          


        [HttpPost]
        public ActionResult Ret(LoginTabel retbruger)
        {
            //Hvis Modellen accepterer de nye input, så smid view til Index
            if (ModelState.IsValid)
            {
                //Skab et hash fra HashSalt.cs
                string NytSalt = HashSalt.GetRandomSalt();

                //sæt NytSalt ind i tabellen
                retbruger.Salt = NytSalt; 
                //sæt hashet NytSalt+kodeord ind i tabellen
                retbruger.Password = HashSalt.HashPassword(retbruger.Password, NytSalt);
                
                db.Entry(retbruger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(retbruger);            
        }

        public ActionResult Slet(int? id)
        {
           //slår en bruger op på ID
            LoginTabel bruger = db.LoginTabel.Find(id);
         
            return View(bruger);
        }

        [HttpPost]
        public ActionResult Slet(int id)
        {
            LoginTabel bruger = db.LoginTabel.Find(id);
            db.LoginTabel.Remove(bruger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      
    }
}
