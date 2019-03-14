using PotteryWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PotteryWeb.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["LoginBruger"] == null)
            {
                filterContext.Result = new RedirectResult("~/login/");
            }
        }
        dbPotteryEntities db = new dbPotteryEntities();
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }
        //*********************************----PRODUKT-----*************************************
        public ActionResult ProductRev()
        {
            List<tbProduct> AI = db.tbProduct.ToList();
            return View(AI);
       
        }
        //*********************************OPRET PRODUKT*************************************
        public ActionResult POpret()
        {
            VM Ni = new VM();
            Ni.tbProduct = new tbProduct();
            Ni.CategoryList = db.tbCategory.ToList();
            return View(Ni);
        }
        [HttpPost]
        public ActionResult POpret(VM NewItem)
        {
            HttpPostedFileBase ProductImg = Request.Files["tbProduct.ProductImg"];
            if (!ModelState.IsValid || ProductImg == null)
            {
                ViewBag.Besked = "Noget gik galt - prøv igen";
                NewItem.CategoryList = db.tbCategory.ToList();
                return View(NewItem); // returner med mangelfuld indtastning.
            }

            //til at gennem fil navn i db og gemme filen i content/img mappen.
            string imgName = System.IO.Path.GetFileName(ProductImg.FileName);
            string imgSti = System.IO.Path.Combine(Server.MapPath("~/Content/img/"), imgName);

            ProductImg.SaveAs(imgSti);

            NewItem.tbProduct.ProductImg = imgName;

            // gem i db - ud fra VM
            db.tbProduct.Add(NewItem.tbProduct);
            db.SaveChanges();


            TempData["besked"] = "item oprettet";
            return RedirectToAction("ProductRev", "Admin");

        }
        //*********************************SLET PRODUKT**************************************
        public ActionResult PSlet(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ProductRev", "Admin");
            }
            tbProduct SI = db.tbProduct.Find(id);

            if (SI == null)
                return RedirectToAction("ProductRev", "Admin");

            return View(SI);
        }
        [HttpPost]
        public ActionResult PSlet(int id)
        {
            tbProduct SI = db.tbProduct.Find(id);
            if (SI == null)
                return RedirectToAction("ProductRev", "Admin");

            db.tbProduct.Remove(SI);
            db.SaveChanges();
            return RedirectToAction("ProductRev", "Admin");
        }
        //**********************************RET PRODUKT**************************************
        public ActionResult PRet(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ProductRev", "Admin");
            }
            VM RI = new VM();

            RI.tbProduct = db.tbProduct.Find(id);
            if (RI == null)
                return RedirectToAction("ProductRev", "Admin");
            RI.CategoryList = db.tbCategory.ToList();

            return View(RI);
        }
        [HttpPost]
        public ActionResult PRet(VM id, HttpPostedFileBase NytImg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "Noget gik galt - prøv igen";
                id.CategoryList = db.tbCategory.ToList();
                return View(id); // returner med mangelfuld indtastning.
            }
            if (NytImg != null)
            {
                string imgSti = System.IO.Path.Combine(Server.MapPath("~/Content/img/"), id.tbProduct.ProductImg);

                if (System.IO.File.Exists(imgSti))
                    System.IO.File.Delete(imgSti);

                string imgName = System.IO.Path.GetFileName(NytImg.FileName);
                imgSti = System.IO.Path.Combine(Server.MapPath("~/Content/img/"), imgName);

                NytImg.SaveAs(imgSti);
                id.tbProduct.ProductImg = imgName;
            }
            // gem i db
            db.tbProduct.AddOrUpdate(id.tbProduct);
            db.SaveChanges();

            TempData["Besked"] = "Din rettelse er opdateret";
            return RedirectToAction("ProductRev", "Admin");
        }


        //*********************************-----Category-----*************************************

        public ActionResult CategoryRev()
        {
            List<tbCategory> AI = db.tbCategory.ToList();
            return View(AI);

        }
        //*********************************OPRET Category*************************************
        public ActionResult COpret()
        {
            tbCategory NI = new tbCategory();
            return View(NI);
        }
        [HttpPost]
        public ActionResult COpret(tbCategory NewItem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "Noget gik galt - prøv igen";
                return View(NewItem); // returner med mangelfuld indtastning.
            }
            // gem i db
            db.tbCategory.Add(NewItem);
            db.SaveChanges();


            TempData["besked"] = "Kategori er oprettet";
            return RedirectToAction("CategoryRev", "Admin");

        }

        //**********************************RET KATEGORI**************************************
        public ActionResult CRet(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("CategoryRev", "Admin");
            }
            tbCategory RI = new tbCategory();

            RI = db.tbCategory.Find(id);
            if (RI == null)
                return RedirectToAction("CategoryRev", "Admin");

            return View(RI);
        }
        [HttpPost]
        public ActionResult CRet(tbCategory id)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "Noget gik galt - prøv igen";
 
                return View(id); // returner med mangelfuld indtastning.
            }
           
            // gem i db
            db.tbCategory.AddOrUpdate(id);
            db.SaveChanges();

            TempData["Besked"] = "Din rettelse er opdateret";
            return RedirectToAction("CategoryRev", "Admin");
        }

        //*********************************SLET PRODUKT**************************************
        public ActionResult CSlet(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Category", "Admin");
            }
            tbCategory SI = db.tbCategory.Find(id);

            if (SI == null)
                return RedirectToAction("Category", "Admin");

            return View(SI);
        }
        [HttpPost]
        public ActionResult CSlet(int id)
        {
            tbCategory SI = db.tbCategory.Find(id);
            if (SI == null)
                return RedirectToAction("CategoryRev", "Admin");

            if(SI.tbProduct != null)
            {
                TempData["Besked"] = "Du kan ikke slette en kategory hvor i der er tilhørende produkter. slet eller flyt produkterne i denne kategori";
                return RedirectToAction("CategoryRev", "Admin");
            }

            db.tbCategory.Remove(SI);
            db.SaveChanges();
            return RedirectToAction("CategoryRev", "Admin");
        }
    }
}