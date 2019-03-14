using ProductWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductWeb.Controllers
{

    public class HomeController : Controller
    {
        ProductDbEntities1 Db = new ProductDbEntities1();
        // GET: Home
        public ActionResult Index()
        {
            List<ProductTable> ProductList = new List<ProductTable>();

            ProductList = Db.ProductTable.ToList();

            return View(ProductList);

        }
        [ChildActionOnly]
        public ActionResult KategoriMenu()
        {
            List<kategoriTable> KategoriListe = new List<kategoriTable>();
            KategoriListe = Db.kategoriTable.ToList();
            return PartialView("_KategoriMenu", KategoriListe);
        }
        public ActionResult VisProduct(int? Id)
        {
            // Hvis der ikke er en ID med, så spark tilbage til index-siden
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            //Joke UdvalgtJoke = new Joke();
            //UdvalgtJoke = Db.Joke.Where(c => c.JokeId == Id).FirstOrDefault();

            ProductTable UdvalgtProduct = Db.ProductTable.Find(Id);

            // Hvis der ikke er et citat som matcher, så spark tilbage til index-siden
            if (UdvalgtProduct == null)
                return RedirectToAction("Index");

            return View(UdvalgtProduct);
        }
        public ActionResult VisKategorier()
        {
            List<kategoriTable> KategoriListe = new List<kategoriTable>();
            KategoriListe = Db.kategoriTable.ToList();
            return View(KategoriListe);

        }
        public ActionResult VisProductUdfraKategori(int Id)
        {

            List<ProductTable> JokeListe = new List<ProductTable>();
            JokeListe = Db.ProductTable.Where(j => j.FK_KategoriID == Id).ToList();
            return View(JokeListe);

        }
    }
}