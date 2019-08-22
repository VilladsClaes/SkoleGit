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
    public class SizeTablesController : Controller
    {
        private SkoEntities db = new SkoEntities();

        // GET: SizeTables
        public ActionResult Index()
        {
            return View(db.SizeTables.ToList());
        }

        // GET: SizeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeTable sizeTable = db.SizeTables.Find(id);
            if (sizeTable == null)
            {
                return HttpNotFound();
            }
            return View(sizeTable);
        }

        // GET: SizeTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SizeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeID,SizeNumber")] SizeTable sizeTable)
        {
            if (ModelState.IsValid)
            {
                db.SizeTables.Add(sizeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sizeTable);
        }

        // GET: SizeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeTable sizeTable = db.SizeTables.Find(id);
            if (sizeTable == null)
            {
                return HttpNotFound();
            }
            return View(sizeTable);
        }

        // POST: SizeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeID,SizeNumber")] SizeTable sizeTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sizeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sizeTable);
        }

        // GET: SizeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeTable sizeTable = db.SizeTables.Find(id);
            if (sizeTable == null)
            {
                return HttpNotFound();
            }
            return View(sizeTable);
        }

        // POST: SizeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SizeTable sizeTable = db.SizeTables.Find(id);
            db.SizeTables.Remove(sizeTable);
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
