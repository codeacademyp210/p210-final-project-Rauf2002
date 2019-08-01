using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Areas.Admin.Controllers
{
    public class GalleryController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Admin/Gallery
        public ActionResult Index()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                return View(db.Gallery.ToList());
            }
            return RedirectToAction("Index", "Login");
        }

        

        // GET: Admin/Gallery/Create
        public ActionResult Create()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
                
        }

        // POST: Admin/Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameAz,NameEng,Photo")] Gallery gallery,HttpPostedFileBase Photo)
        {
            string image = "";
            string photoName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Photo.FileName;

            if (ModelState.IsValid)
            {
                image = Path.Combine(Server.MapPath("~/Uploads/Gallery"), photoName);
                Photo.SaveAs(image);
                gallery.Photo = photoName;
                db.Gallery.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // GET: Admin/Gallery/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Gallery gallery = db.Gallery.Find(id);
                if (gallery == null)
                {
                    return HttpNotFound();
                }
                return View(gallery);
            }
            return RedirectToAction("Index", "Login");
               
        }

        // POST: Admin/Gallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameAz,NameEng,Photo")] Gallery gallery, HttpPostedFileBase Photo)
        {
            string image = "";
            string photoName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Photo.FileName;

            if (ModelState.IsValid)
            {
                image = Path.Combine(Server.MapPath("~/Uploads/Gallery"), photoName);
                Photo.SaveAs(image);
                gallery.Photo = photoName;
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }


        public ActionResult Delete(int Id)
        {
            Models.Gallery foundGallery= db.Gallery.FirstOrDefault(g => g.Id == Id);

            if (foundGallery != null)
            {
                db.Gallery.Remove(foundGallery);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
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
