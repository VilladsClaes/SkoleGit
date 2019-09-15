using ProductWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductWeb.Controllers
{

    public class AdminProductController : Controller
    {
        ProductDbEntities1 Db = new ProductDbEntities1();
        // GET: AdminProduct
        public ActionResult Index()
        {
            List<ProductTable> ProductList = new List<ProductTable>();
            ProductList = Db.ProductTable.ToList();
            return View(ProductList);
        }

        public ActionResult OpretNyProduct()
        {
            AdminProductViewModel NyProduct = new AdminProductViewModel();
            NyProduct.Product = new ProductTable();
            NyProduct.KategoriListe = Db.kategoriTable.ToList();
            return View(NyProduct);
        }
        [HttpPost]
        public ActionResult OpretNyProduct(AdminProductViewModel NyProduct)
        {
            HttpPostedFileBase Photo = Request.Files["Product.Photo"];
            if (!ModelState.IsValid || Photo == null)
            {
                ViewBag.Besked = "Noget gik galt,Prøv igen....";
                NyProduct.KategoriListe = Db.kategoriTable.ToList();
                return View(NyProduct);// retur til formularen - send den allerede- udfyldt med retur
            }


            //formularen er korrekt udfyldt -data gemmes i Db og fil uploads i korrekt mappe i webprojekt

            // Håndtere filen (snup filnavn til Db + gem filen i img mappe)
            string ImgNavn = System.IO.Path.GetFileName(Photo.FileName);
            string ImgSti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/"),ImgNavn);


            // gem filen
            Photo.SaveAs(ImgSti);

            // gem i Db
            NyProduct.Product.Photo = ImgNavn; // fx hest.jpg
            Db.ProductTable.Add(NyProduct.Product);
            Db.SaveChanges();



            TempData["Besked"] = "Productet er oprettet";


            return RedirectToAction("Index", "AdminProduct");
        }
        public ActionResult SletProduct(int? Id)

        {
            //Hvis der ikke en Id med, sparke tilbage til index side.
            if (Id == null)
                return RedirectToAction("Index");


            //Hente Cartoon som måske skal slettes, og send den med til viewet.
            ProductTable ProductSlettes = Db.ProductTable.Find(Id);
            if (ProductSlettes == null) //hvis der er ikke Cartoon som matcher Id
                return RedirectToAction("Index");


            return View(ProductSlettes);
        }
        [HttpPost]
        public ActionResult SletProduct(ProductTable ProductSlettes)
        {
            //Hente Cartoon som måske skal slettes. og sende den til viewet...
            //Cartoon har kun en id . så resten skla slås op med "find"
            ProductSlettes = Db.ProductTable.Find(ProductSlettes.ProductID);


            //CartoonTable SC = Db.CartoonTable.Find(Id);

            if (ProductSlettes == null)// hvis der er ikke Cartoon som matcher id 
                return RedirectToAction("Index");

            // slet fysisk fil/img
            string ImgSti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/"), ProductSlettes.Photo);


            // hvis der img i den angivet sti
            // Skal image-filen (jpg, gif, png) slettes hvis produktet slettes?
            //if (System.IO.File.Exists(ImgSti))
            //    System.IO.File.Delete(ImgSti);// imgsti slettet


            // slet fra Db
            Db.ProductTable.Remove(ProductSlettes);
            Db.SaveChanges();

            TempData["Besked"] = "Product er slettet";
            return RedirectToAction("Index", "AdminProduct");
        }







        public ActionResult RetProduct(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index");

            AdminProductViewModel ProductRettes = new AdminProductViewModel();
            ProductRettes.Product = Db.ProductTable.Find(Id);

            if (ProductRettes == null)
                return RedirectToAction("Index");

            ProductRettes.KategoriListe = Db.kategoriTable.ToList();

            return View(ProductRettes);
        }



        [HttpPost]
        public ActionResult RetProduct(AdminProductViewModel ProductRettes, HttpPostedFileBase NyPhoto)
        {
             

            if (!ModelState.IsValid)
            {
                ViewBag.Besked = "Der er noget galt,Prøve igen.....";
                ProductRettes.KategoriListe = Db.kategoriTable.ToList();
                return View(ProductRettes);// return til formularen. send den allrede udfyldt med retur,
            }

            if (NyPhoto != null)
            {
                string imgsti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/"), ProductRettes.Product.Photo);

                // Img-filen (jpg, gif, png osv i Img-mappen): 
                // Udkommenter disse to linjer, hvis image IKKE skal slettes når der bliver valgt et andet image
                //if (System.IO.File.Exists(imgsti))
                //    System.IO.File.Delete(imgsti);// imgsti slettet



                string ImgNavn = System.IO.Path.GetFileName(NyPhoto.FileName);
                 imgsti = System.IO.Path.Combine(Server.MapPath("~/Content/Img/"), ImgNavn);

                NyPhoto.SaveAs(imgsti);//gemme selve filen
                ProductRettes.Product.Photo = ImgNavn; // gemme navnet i modellen -som næste step sends til Db
            }
            Db.ProductTable.AddOrUpdate(ProductRettes.Product);
            Db.SaveChanges();

            TempData["besked"] = "Product er Rettet";
            return RedirectToAction("Index", "AdminProduct");
        }
    }
}