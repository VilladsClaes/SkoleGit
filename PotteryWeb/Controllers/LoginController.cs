using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    public class LoginController : Controller
    {
        dbPotteryEntities db = new dbPotteryEntities();
        // GET: Login
        public ActionResult Index()
        {
            Session.Remove("loginBruger");
            return View();
        }

        [HttpPost]
        public ActionResult Index(tbUser B)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            tbUser BrugerMatch = new tbUser();
            BrugerMatch = db.tbUser.Where(b => b.Username == B.Username && b.Password == B.Password).FirstOrDefault();

            //Der er ikke noget match. Brugermatch == null
            if (BrugerMatch == null)
            {
                Session.Remove("loginBruger");
                ViewBag.Besked = "Der er ikke match mellem brugernavn og password";
                return View();
            }
            //hvis der var et match
            Session["loginBruger"] = BrugerMatch.Name;

            return RedirectToAction("Index", "Admin");
        }
    }
}