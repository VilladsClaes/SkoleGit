using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCcitater.Models;

namespace MVCcitater.Controllers
{
    public class ADMINController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //
            if(Session["BurgerSession"] == null)
            {
                filterContext.Result = new RedirectResult("/LogInd");
            }
        }
        // GET: ADMIN
        // *******ADMIN startside**********
        // **************************

        DbCitaterEntities Db = new DbCitaterEntities();



        public ActionResult Index()
        {
            List<Citater> CitatList = new List<Citater>();
            CitatList = Db.Citater.ToList();

            return View(CitatList);
        }



       
        // *******Opretstartside**********
        // **************************
        public ActionResult OpretCitat()
        {
            Citater NytCitat = new Citater();
            return View(NytCitat);
        }

        [HttpPost]


        public ActionResult OpretCitat(Citater NytCitat)
        {
            //Gem Citat i db- og tjek evt.førest om data valid



            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "Noget er galt- Prøv igen";
                return View(NytCitat);
            }

            Db.Citater.Add(NytCitat);
            Db.SaveChanges();

            //ViewBag.Besked = "Citat er oprettet";
            TempData["Besked"] = "Citat er oprettet";
            return RedirectToAction("Index","ADMIN");
        }
        // *******Slet Citat**********
        // **************************


        public ActionResult SletCitat(int? Id)
        {

            //Hvis der ikke Id Tal så redircte vi til Index side.....
            if (Id == null)
                return RedirectToAction("Index");

            //Hent Citat som har samme Id

            Citater CitatSlet = Db.Citater.Find(Id);


            return View(CitatSlet);
        }

        [HttpPost]
        public ActionResult SletCitat(int CitatID) 
        {
            Citater FjernCitat = Db.Citater.Find(CitatID);
            if(FjernCitat == null) //Hvis der er ikke et Citat som matcher id 
                return RedirectToAction("Index");
            Db.Citater.Remove(FjernCitat);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RetCitat(int?id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Citater RetCitat = Db.Citater.Find(id);


            return View(RetCitat);
        }

        [HttpPost]
        public ActionResult RetCitat(Citater CitatRetted)
        {


            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "Noget er galt- Prøv igen";
                return View(CitatRetted);
            }

            Db.Citater.AddOrUpdate(CitatRetted);
            Db.SaveChanges();

            //ViewBag.Besked = "Citat er rettet";
            TempData["Besked"] = "Citat er rettet";
            return RedirectToAction("Index", "ADMIN");
        }
    }
}