﻿using JavaS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace JavaS.Controllers
{
    public class jQueryController : Controller
    {
       
        
        public ActionResult opg01()
        {
            return View();
        }
        public ActionResult opg02()
        {
            return View();
        }
        public ActionResult opg03()
        {
            return View();
        }
        public ActionResult opg04()
        {
            return View();
        }
        public ActionResult opg05()
        {
            return View();
        }
        public ActionResult opg06()
        {
            return View();
        }
        public ActionResult opg07()
        {
            return View();
        }
        public ActionResult opg08()
        {
            return View();
        }
        public ActionResult opg09()
        {
            return View();

        }
        public ActionResult opg10()
        {
            return View();
        }

        PopopDatabaseEntities db = new PopopDatabaseEntities();
        public ActionResult Popup()
        {
            //Instantiér en liste items og fyld den med indholdet fra databasen som en liste
            List<Models.tblPopup> items = db.tblPopup.ToList();

            return View(items);
        }
        public ActionResult slider()
        {
            return View();
        }

        public ActionResult Vendespil()
        {    
            return View();
        }
    }
}