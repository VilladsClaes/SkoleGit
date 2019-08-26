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
        TegnefilmsfigurerEntities Db = new TegnefilmsfigurerEntities();
        // GET: Home
        public ActionResult Index()
        {
            object[] ArrayOfCartoons;

            ArrayOfCartoons = Db.TegnefilmsfigurTabel.ToArray();

            List<TegnefilmsfigurTabel> CartoonList = new List<TegnefilmsfigurTabel>();
            CartoonList = Db.TegnefilmsfigurTabel.ToList();

            for (int i = 0; i < ArrayOfCartoons.Length; i++)
            {
                ArrayOfCartoons[i].ToString();
            }


            return View(CartoonList);
        }
        public ActionResult CartoonInfo(int? Id)
        {
            if (Id == null)
                RedirectToAction("Index");
            TegnefilmsfigurTabel ChoosedCartoon = new TegnefilmsfigurTabel();
            ChoosedCartoon = Db.TegnefilmsfigurTabel.Where(c => c.TegnefilmsfigurID == Id).FirstOrDefault();

            if (ChoosedCartoon == null)
            {
                return View("Index");

            }
            return View(ChoosedCartoon);

           
        }
    }
}