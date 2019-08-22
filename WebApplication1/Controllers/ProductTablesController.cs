using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductTablesController : Controller
    {
        private SkoEntities db = new SkoEntities();

        // GET: ProductTables
        public ActionResult Index()
        {
            var productTables = db.ProductTables.Include(p => p.BrandTable);
            return View(productTables.ToList());
        }

        // GET: ProductTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTable productTable = db.ProductTables.Find(id);
            if (productTable == null)
            {
                return HttpNotFound();
            }
            return View(productTable);
        }

        // GET: ProductTables/Create
        public ActionResult Create()
        {
            ViewBag.ProductBrandID = new SelectList(db.BrandTables, "BrandID", "BrandName");
            return View();
        }

        // POST: ProductTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductPrice,ProductBrandID,ProductBeskrivelse")] ProductTable productTable, List<HttpPostedFileBase> PictureFileNames)
        {
            if (ModelState.IsValid)
            {
                Guid FileId = Guid.NewGuid(); //Navngiver filerne med et unikt navn for at undgå at billederne hedder det samme.


                //Definerer listen af extentions/filudvidelser/filformater som er tilladt i uploaderen

                string[] Ex = { ".jpg", ".png", ".gif" };

                if (PictureFileNames != null)
                {
                    foreach (HttpPostedFileBase PictureFileName in PictureFileNames)
                    {

                    }
                    IOTools.FileUplader("~/Uploads/ProductPictures", PictureFileName, Ex, FileId.ToString());
                    pictureTable.PictureFileName = FileId.ToString() + "_" + PictureFileName.FileName;
                    db.PictureTables.Add(pictureTable);
                }
                  
                
                db.ProductTables.Add(productTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductBrandID = new SelectList(db.BrandTables, "BrandID", "BrandName", productTable.ProductBrandID);
            return View(productTable);
        }

        // GET: ProductTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTable productTable = db.ProductTables.Find(id);
            if (productTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductBrandID = new SelectList(db.BrandTables, "BrandID", "BrandName", productTable.ProductBrandID);
            return View(productTable);
        }

        // POST: ProductTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductPrice,ProductBrandID,ProductBeskrivelse")] ProductTable productTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductBrandID = new SelectList(db.BrandTables, "BrandID", "BrandName", productTable.ProductBrandID);
            return View(productTable);
        }

        // GET: ProductTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTable productTable = db.ProductTables.Find(id);
            if (productTable == null)
            {
                return HttpNotFound();
            }
            return View(productTable);
        }

        // POST: ProductTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductTable productTable = db.ProductTables.Find(id);
            db.ProductTables.Remove(productTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
