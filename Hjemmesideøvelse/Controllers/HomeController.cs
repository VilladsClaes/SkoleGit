using Hjemmesideøvelse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hjemmesideøvelse.Controllers
{
    public class HomeController : Controller
    {
        TegnefilmsfigurerEntities Db = new TegnefilmsfigurerEntities();
        // GET: Home
        public ActionResult Index()
        {
            

            List<TegnefilmsfigurTabel> CartoonList = new List<TegnefilmsfigurTabel>();
            CartoonList = Db.TegnefilmsfigurTabels.ToList();

           

            return View(CartoonList);
        }
        public ActionResult CartoonInfo(int? Id)
        {
            if (Id == null)
                RedirectToAction("Index");
            TegnefilmsfigurTabel ChoosedCartoon = new TegnefilmsfigurTabel();
            ChoosedCartoon = Db.TegnefilmsfigurTabels.Where(c => c.TegnefilmsfigurID == Id).FirstOrDefault();

            if (ChoosedCartoon == null)
            {
                return View("Index");

            }
            return View(ChoosedCartoon);

           
        }
    }
}