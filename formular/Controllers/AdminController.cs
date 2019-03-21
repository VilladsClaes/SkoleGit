using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace formular.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //if(Session["Login"]!= null)
            //{
            //    return View();
            //}
            //return RedirectToAction("LoginPage", "Home");
            return View();
        }
        public ActionResult Logud()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Admin");

        }
        public ActionResult AddProduct()
        {
         
            return View();
        }
    }
}