using CartoonSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CartoonSite.Controllers
{
    public class HomeController : Controller
    {
        CartoonDbEntities1 Db = new CartoonDbEntities1();
        // GET: Home
        public ActionResult Index()
        {
            List<CartoonTable> CartoonList = new List<CartoonTable>();
            CartoonList = Db.CartoonTable.ToList();
            return View(CartoonList);
        }
        public ActionResult CartoonInfo(int? Id)
        {
            if (Id == null)
                RedirectToAction("Index");
            CartoonTable ChoosedCartoon = new CartoonTable();
            ChoosedCartoon = Db.CartoonTable.Where(c => c.CartoonID == Id).FirstOrDefault();

            if (ChoosedCartoon == null)
            {
                return View("Index");

            }
            return View(ChoosedCartoon);

           
        }
    }
}