using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Skabelon;


namespace ASP_MVC_Skabelon.Controllers
{
    public class ProductController : Controller
    {
        

        ASP_MVC_Skabelon.Models.ASP_MVC_SkabelonEntities db = new Models.ASP_MVC_SkabelonEntities();
   
        public ActionResult Index()
        {
            //Vis alle produkterne i en liste
            ASP_MVC_Skabelon.Models.ProductViewModel vm = new Models.ProductViewModel();

            vm.Products = db.ProductTable.ToList();

            return View(vm.Products);

        }
        [ChildActionOnly]
        public ActionResult _KategoriMenu()
        {
            //Vis alle kategorierne i en liste
            Models.ProductViewModel vm = new Models.ProductViewModel();
            vm.Categories = db.ProductCategoryTable.ToList();
           
            return PartialView("_KategoriMenu", vm);
        }

        public ActionResult VisProduct(int? Id)
        {
            //Vis et enkelt produkt på ID
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            Models.ProductViewModel vm = new Models.ProductViewModel();
            vm.Product = db.ProductTable.Find(Id);

            // Hvis der ikke er et citat som matcher, så spark tilbage til index-siden
            if (vm.Product == null)
                return RedirectToAction("Index");

            return View(vm);
        }

        
        public ActionResult VisProductUdfraKategori(int Id)
        {
            //Vis produkterne indenfor en valgt kategori
            Models.ProductViewModel vm = new Models.ProductViewModel();
            vm.Products = db.ProductTable.Where(j => j.FK_ProductCategoryID == Id).ToList();
            return View(vm);

        }


    }
}