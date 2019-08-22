using reltest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace reltest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        SkoEntities db = new SkoEntities();
 
        

        public ActionResult Index()
        {

            return View(db.ProductTables.ToList());
        }


        [HttpPost]
        public ActionResult Index(ProductTable NewProduct, List<HttpPostedFileBase> ProductImages)
        {

            string[] EX = { ".jpg", ".png", ".gif" };
            Guid FileId = Guid.NewGuid();

            if (ProductImages != null)
            {
                foreach (HttpPostedFileBase imgFile in ProductImages)
                {

                    PictureTable NewImg = new PictureTable();

                    IOTools.FileUplader("/Uploads", imgFile, EX, FileId.ToString());
                    NewImg.PictureFileName = FileId.ToString() + "_" + imgFile.FileName;

                    NewProduct.PictureTables.Add(NewImg);

                }
            }

            db.ProductTables.Add(NewProduct);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}