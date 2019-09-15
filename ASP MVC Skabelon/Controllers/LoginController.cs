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
        public ASP_MVC_Skabelon.Models.ASP_MVC_SkabelonEntities db = new Models.ASP_MVC_SkabelonEntities();

        public ActionResult Index()
        {
            //Hvis nogen er logget ind skal de logges ud ved denne login-skærm
            Session.Remove("BrugerSession");
            //Login-forsiden viser en liste med indholdet af brugeroplysninger
            return View(db.LoginTable.ToList());
        }

        [HttpPost]
        public ActionResult Index(ASP_MVC_Skabelon.Models.LoginTable LogIndBruger)
        {
            //hvis modellen er udfyldt med fejl/mangler- send bruger tilbage til viwet ( her er samme side )
            if (!ModelState.IsValid)
                return View(db.LoginTable.ToList());
            // Find ud af om der er match
            LoginTable BrugerMatch = new LoginTable();
            //find brugeren baseret på brugernavn
            LoginTable BrugerNavnet = new LoginTable();
            //Brugernavnet slåes op i tabellen
            BrugerNavnet = db.LoginTable.Where(b => b.username == LogIndBruger.username).FirstOrDefault();
            //Hvis brugernavnet findes i tabellen
            if (BrugerNavnet != null)
            {
                //
                string passwordOgSalt = HashSalt.HashPassword(LogIndBruger.password, BrugerNavnet.salt);
                BrugerMatch = db.LoginTable.Where(b => b.username == LogIndBruger.username && b.password == passwordOgSalt).FirstOrDefault();
                if (BrugerMatch != null)
                {
                    // der blev fundet et match =login = session
                    //der blev lavet en session - som er den vi "måler" på, om brugen skal have adgang til admin

                    Session["BrugerSession"] = BrugerMatch.username;


                    return RedirectToAction("Index", "Admin"); // bruger sendes til startsiden på ADMIN
                }
                else
                {
                    // brugermatch == null .....der var ikke noget match



                    // her er mere sikerhed for logind da vi sletter sission hvis der noget forkert.....
                    Session.Remove("BrugerSession");


                    ViewBag.Besked = "UPS! forkert brugernavn og/eller Password Prøv igen";
                    return View(db.LoginTable.ToList()); // retun samme side med beskden.
                }
            }
            else
            {

                // brugermatch == null .....der var ikke noget match



                // her er mere sikerhed for logind da vi sletter sission hvis der noget forkert.....
                Session.Remove("BrugerSession");


                ViewBag.Besked = "UPS! forkert brugernavn og/eller Password Prøv igen";
                return View(db.LoginTable.ToList()); // retun samme side med beskden.
            }

           
            


          
        }


        public ActionResult Vis(int? id)
        {
            //Login-detaljevisningen viser en bruger baseret på det id der søges på
            ASP_MVC_Skabelon.Models.LoginTable brugere = db.LoginTable.Find(id);
           
            return View(brugere);
        }

        public ActionResult Opret()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Opret(LoginTable Nybruger)
        {
            if (ModelState.IsValid)
            {
                //tager metode fra cs-filen som vi har lavet
                string UniktSalt = HashSalt.GetRandomSalt();
                //lægger denne string ind i tabellen
                Nybruger.salt = UniktSalt;
                //tager brugerens password, kombinerer det med saltet, bruger SHA-algoritmen på det og lægger denne kode ind  i tabellen
                Nybruger.password = HashSalt.HashPassword(Nybruger.password, UniktSalt); 

                db.LoginTable.Add(Nybruger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Nybruger);
        }

        public ActionResult Ret(int? id)
        {
            //slår en bruger op baseret på det ID der søges
            LoginTable bruger = db.LoginTable.Find(id);
            
            return View(bruger);
        }


        
          


        [HttpPost]
        public ActionResult Ret(LoginTable retbruger)
        {
            //Hvis Modellen accepterer de nye input, så smid view til Index
            if (ModelState.IsValid)
            {
                //Skab et hash fra HashSalt.cs
                string NytSalt = HashSalt.GetRandomSalt();

                //sæt NytSalt ind i tabellen
                retbruger.salt = NytSalt; 
                //sæt hashet NytSalt+kodeord ind i tabellen
                retbruger.password = HashSalt.HashPassword(retbruger.password, NytSalt);
                
                db.Entry(retbruger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(retbruger);            
        }

        public ActionResult Slet(int? id)
        {
            //slår en bruger op på ID
            LoginTable bruger = db.LoginTable.Find(id);
         
            return View(bruger);
        }

        [HttpPost]
        public ActionResult Slet(int id)
        {
            LoginTable bruger = db.LoginTable.Find(id);
            db.LoginTable.Remove(bruger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

      
    }
}
