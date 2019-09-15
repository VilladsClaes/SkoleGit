using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Skabelon;

namespace ASP_MVC_Skabelon.Controllers
{
    public class AdminController : Controller
    {
        //Tillad kun indgang hvis der er en cookie oprettet
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
            if (Session["BrugerSession"] == null)
            {
                filterContext.Result = new RedirectResult("/Login");
            }
        }

        
        ASP_MVC_Skabelon.Models.ASP_MVC_SkabelonEntities db = new Models.ASP_MVC_SkabelonEntities();
      
        //
        public ActionResult Index()
        {
            //Vis alle produkterne i en liste ved hjælp af den allerede instantierede List<Product> Products
            Models.ProductViewModel vm = new Models.ProductViewModel();

            vm.Products = db.ProductTable.ToList();

            return View(vm);

            
        }

        //Disse CRUD-actions 
        public ActionResult OpretNyProduct()
        {
            Models.ProductViewModel vm = new Models.ProductViewModel();

            vm.Product = new Models.ProductTable();
            vm.Products = db.ProductTable.ToList();
            vm.Categories = db.ProductCategoryTable.ToList();
            vm.Photos = db.ProductPhotoTable.ToList();

            return View(vm);
        }
        [HttpPost]
        public ActionResult OpretNyProduct(Models.ProductViewModel vm, List<HttpPostedFileBase> Produktbilleder)
        {
            Models.ProductViewModel Nytprodukt = new Models.ProductViewModel();
            Nytprodukt.Product = new Models.ProductTable();
            Nytprodukt.Photo = new Models.ProductPhotoTable();
            Nytprodukt.Photos = db.ProductPhotoTable.ToList();

            string[] TilladteFilformater = { ".jpg", ".png", ".gif" };

            if (!ModelState.IsValid)
            {

                ViewBag.Besked = "Din model er ufin..";
                return View(vm);// retur til formularen - send den allerede- udfyldt med retur
            }

            if ( Produktbilleder != null)
            {
               
                
                //Uploader flere billeder til List<>
                foreach (HttpPostedFileBase item in Produktbilleder)
                {
                    Guid randomnavn = Guid.NewGuid();                    
                  
                    IOTools.FileUplader("/Content/grafik/ProduktBilleder", randomnavn.ToString(), item, TilladteFilformater);
                    Nytprodukt.Photo.Foto = randomnavn.ToString() + item.FileName;
                    Nytprodukt.Photos.Add(Nytprodukt.Photo);
                    db.ProductPhotoTable.Add(Nytprodukt.Photo);
                }
            }
            else     
            {

                ViewBag.Besked = "No billeder....";               

                return View(vm);// retur til formularen - send den allerede- udfyldt med retur
            }


            Nytprodukt = vm;
            
            // gem i Db
            db.ProductTable.Add(Nytprodukt.Product);
           
            db.SaveChanges();



            TempData["Besked"] = "Produktet er oprettet";


            return RedirectToAction("Index", "Admin");
        }

        public ActionResult SletProduct(int? Id)
        {            
            if (Id == null)
                return RedirectToAction("Index");
            
            Models.ProductTable ProductSlettes = db.ProductTable.Find(Id);

            if (ProductSlettes == null)
                return RedirectToAction("Index");

            return View(ProductSlettes);
        }
        [HttpPost]
        public ActionResult SletProduct(Models.ProductViewModel vm)
        {            
            vm.Product = db.ProductTable.Find(vm.Product.ID);            

            if (vm == null)
                return RedirectToAction("Index");           
                       
            // slet fra Db
            db.ProductTable.Remove(vm.Product);
            db.SaveChanges();

            TempData["Besked"] = "Product er slettet";
            return RedirectToAction("Index", "Admin");
        }   
        public ActionResult RetProduct(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index");

            Models.ProductViewModel vm = new  Models.ProductViewModel();
            vm.Product = db.ProductTable.Find(Id);

            if (vm == null)
                return RedirectToAction("Index");

            vm.Categories = db.ProductCategoryTable.ToList();
            vm.Photos = db.ProductPhotoTable.ToList();

            return View(vm);
        }



        [HttpPost]
        public ActionResult RetProduct(Models.ProductViewModel vm, List<HttpPostedFileBase> Produktbilleder)
        {

            string[] TilladteFilformater = { ".jpg", ".png", ".gif" };

            if (!ModelState.IsValid)
            {

                ViewBag.Besked = "Din model er ufin..";
                return View(vm);
            }

            if (Produktbilleder != null)
            {
                Guid randomnavn = Guid.NewGuid();

                //Uploader flere billeder til List<>
                foreach (HttpPostedFileBase item in Produktbilleder)
                {
                    Models.ProductPhotoTable pictures = new Models.ProductPhotoTable();
                    IOTools.FileUplader("/Content/grafik/ProduktBilleder", randomnavn.ToString(), item, TilladteFilformater);
                    pictures.Foto = randomnavn.ToString() + item.FileName;
                    vm.Photos.Add(pictures);
                }
            }
            else
            {

                ViewBag.Besked = "No billeder....";

                return View(vm);// retur til formularen - send den allerede- udfyldt med retur
            }



            db.ProductTable.AddOrUpdate(vm.Product);
            db.SaveChanges();

            TempData["besked"] = "Product er Rettet";
            return RedirectToAction("Index", "Admin");
        }
    }
}