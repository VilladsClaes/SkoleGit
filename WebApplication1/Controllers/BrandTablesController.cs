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
    public class BrandTablesController : Controller
    {
        private SkoEntities db = new SkoEntities();

        // GET: BrandTables
        public ActionResult Index()
        {
            return View(db.BrandTables.ToList());
        }

        // GET: BrandTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandTable brandTable = db.BrandTables.Find(id);
            if (brandTable == null)
            {
                return HttpNotFound();
            }
            return View(brandTable);
        }

        // GET: BrandTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandTables/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandName,BrandLogo")] BrandTable brandTable, HttpPostedFileBase BrandLogo)
        {
            if (ModelState.IsValid)
            {
                Guid FileId = Guid.NewGuid(); //Navngiver filerne med et unikt navn for at undgå at billederne hedder det samme.

                //Definerer listen af extentions/filudvidelser/filformater som er tilladt i uploaderen
                string[] Ex = { ".jpg", ".png", ".gif" };
                IOTools.FileUplader("~/Uploads", BrandLogo, Ex, FileId.ToString());
                brandTable.BrandLogo = FileId.ToString() + "_" + BrandLogo.FileName;
                db.BrandTables.Add(brandTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brandTable);
        }

        // GET: BrandTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandTable brandTable = db.BrandTables.Find(id);
            if (brandTable == null)
            {
                return HttpNotFound();
            }
            return View(brandTable);
        }

        // POST: BrandTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandID,BrandName,BrandLogo")] BrandTable brandTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brandTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brandTable);
        }

        // GET: BrandTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandTable brandTable = db.BrandTables.Find(id);
            if (brandTable == null)
            {
                return HttpNotFound();
            }
            return View(brandTable);
        }

        // POST: BrandTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BrandTable brandTable = db.BrandTables.Find(id);
            db.BrandTables.Remove(brandTable);
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
