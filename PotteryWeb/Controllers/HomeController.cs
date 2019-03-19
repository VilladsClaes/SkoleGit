using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    public class HomeController : Controller
    {
        dbPotteryEntities db = new dbPotteryEntities();
            // GET: Home
        public ActionResult Index()
        {

            List<tbProduct> ItemList = db.tbProduct.ToList();

            return View(ItemList);
        }

        public ActionResult TheItem(int? Id)
        {
            //hvis der ikke et id med.
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            tbProduct Item =  db.tbProduct.Where(c => c.IdProduct == Id).FirstOrDefault();

            if (Item == null)
            {
                return RedirectToAction("Index");
            }

            return View(Item);

           
        }
        
        public ActionResult ByPrice()
        {
            List<tbProduct> ProductList = new List<tbProduct>();
            ProductList = db.tbProduct.OrderBy(p => p.Price).Take(5).ToList();

            return View(ProductList);
          
        }


    }

}