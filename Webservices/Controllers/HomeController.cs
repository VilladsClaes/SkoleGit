﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webservices.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Starwars()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Byvejr()
        {
            return View();
        }

        public ActionResult PostnummerOpslag()
        {
            return View();
        }

        public ActionResult FilmAPI()
        {
            return View();
        }

        public ActionResult Folketinget()
        {
            return View();
        }
    }
}