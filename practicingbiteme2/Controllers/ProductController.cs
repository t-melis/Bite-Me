using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using practicingbiteme2.Models;
using PagedList;
using System.IO;

namespace practicingbiteme2.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index(int? page, string searchString, string category)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);       // αν είναι null η σελίδα, να την κάνεις "1"

            var productCategory = new List<string>();

            var productQry = from p in db.Products
                             orderby p.Category
                             select p.Category.ToString();

            productCategory.AddRange(productQry.Distinct());

            ViewBag.category = new SelectList(productCategory);

            var products = from p in db.Products
                           where p.IsDeleted == false
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.ToString() == category);
            }

            return View(products.OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize));
        }


        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]        // eksaskisi
        public ActionResult Create([Bind(Include = "ProductID,Name,Description,Price,Category,ImageURL")] Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // extract only the filename                    
                    string _FileName = Path.GetFileName(file.FileName);
                    string category = "";
                    if (file != null && file.ContentLength > 0 && file.ContentLength < 8000000)
                    {

                        string _path = Path.Combine(Server.MapPath(""), _FileName);
                        string extention = Path.GetExtension("");
                        switch (product.Category)
                        {
                            case Category.Cake:
                                category = "Cakes";
                                break;
                            case Category.Cookie:
                                category = "Cookies";
                                break;
                            case Category.Pastry:
                                category = "Pastry";
                                break;
                        }
                        _path = Path.Combine(Server.MapPath("~/Photos/Products/"), category, _FileName);
                        extention = Path.GetExtension(_FileName);
                        if (extention == ".jpg" || extention == ".png" || extention == ".pdf")
                        {
                            file.SaveAs(_path);
                        }
                        else
                        {
                            ViewBag.Message = "Only files of the extension (.jpg .png .pdf) are allowed. Please try again.";
                            return View(product);
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Your file is empty. Please try again.";
                        return View(product);
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    product.ImageURL = $"/Photos/Products/{category}/{_FileName}";
                    db.Products.Add(product);
                    db.SaveChanges();
                    return View(product);
                }
                catch
                {
                    ViewBag.Message = "File upload failed!!";
                    return View(product);
                }
            }
            return View(product);
        }


        // GET: Product/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Description,Price,Category,ImageURL")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            product.IsDeleted = true;
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
