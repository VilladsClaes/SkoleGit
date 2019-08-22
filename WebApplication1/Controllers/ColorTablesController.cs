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
    public class ColorTablesController : Controller
    {
        private SkoEntities db = new SkoEntities();

        // GET: ColorTables
        public ActionResult Index()
        {
            return View(db.ColorTables.ToList());
        }

        // GET: ColorTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorTable colorTable = db.ColorTables.Find(id);
            if (colorTable == null)
            {
                return HttpNotFound();
            }
            return View(colorTable);
        }

        // GET: ColorTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ColorTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColorID,ColorHexCode")] ColorTable colorTable)
        {
            if (ModelState.IsValid)
            {
                db.ColorTables.Add(colorTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(colorTable);
        }

        // GET: ColorTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorTable colorTable = db.ColorTables.Find(id);
            if (colorTable == null)
            {
                return HttpNotFound();
            }
            return View(colorTable);
        }

        // POST: ColorTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColorID,ColorHexCode")] ColorTable colorTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colorTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colorTable);
        }

        // GET: ColorTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorTable colorTable = db.ColorTables.Find(id);
            if (colorTable == null)
            {
                return HttpNotFound();
            }
            return View(colorTable);
        }

        // POST: ColorTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ColorTable colorTable = db.ColorTables.Find(id);
            db.ColorTables.Remove(colorTable);
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
