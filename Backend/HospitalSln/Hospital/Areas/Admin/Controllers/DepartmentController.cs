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
    public class DepartmentController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Admin/Department
        public ActionResult Index()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                return View(db.Departments.ToList());
            }
            return RedirectToAction("Index", "Login");
        }



        // GET: Admin/Department/Create
        public ActionResult Create()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameAz,NameEng,Icon,Photo,DesAz,DesEng")] Departments departments,HttpPostedFileBase Photo)
        {
            string image = "";
            string photoName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Photo.FileName;

            if (ModelState.IsValid)
            {
                image = Path.Combine(Server.MapPath("~/Uploads/Departments"), photoName);
                Photo.SaveAs(image);
                departments.Photo = photoName;
                db.Departments.Add(departments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departments);
        }

        // GET: Admin/Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Departments departments = db.Departments.Find(id);
                if (departments == null)
                {
                    return HttpNotFound();
                }
                return View(departments);
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameAz,NameEng,Icon,Photo,DesAz,DesEng")] Departments departments,HttpPostedFileBase Photo)
        {
            string image = "";
            string photoName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Photo.FileName;

            if (ModelState.IsValid)
            {
                image = Path.Combine(Server.MapPath("~/Uploads/Departments"), photoName);
                Photo.SaveAs(image);
                departments.Photo = photoName;
                db.Entry(departments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departments);
        }

        public ActionResult Delete(int Id)
        {
            Models.Departments foundDepartment = db.Departments.FirstOrDefault(d => d.Id == Id);

            if (foundDepartment != null)
            {
                db.Departments.Remove(foundDepartment);
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
