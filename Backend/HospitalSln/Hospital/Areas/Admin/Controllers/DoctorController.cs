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
    public class DoctorController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Admin/Doctor
        public ActionResult Index()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                var doctors = db.Doctors.Include(d => d.Departments);
                return View(doctors.ToList());
            }
            return RedirectToAction("Index", "Login");
        }



        // GET: Admin/Doctor/Create
        public ActionResult Create()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameEng");
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Phone,Address,WorkHours,Salary,DepartmentId,DesAz,DesEng,Photo")] Doctors doctors,HttpPostedFileBase Photo)
        {

            string image = "";
            string photoName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Photo.FileName;

            if (ModelState.IsValid)
            {
                image = Path.Combine(Server.MapPath("~/Uploads/Doctors"), photoName);
                Photo.SaveAs(image);
                doctors.Photo = photoName;
                db.Doctors.Add(doctors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameAz", doctors.DepartmentId);
            return View(doctors);
        }

        // GET: Admin/Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Doctors doctors = db.Doctors.Find(id);
                if (doctors == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameAz", doctors.DepartmentId);
                return View(doctors);
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Phone,Address,WorkHours,Salary,DepartmentId,DesAz,DesEng,Photo")] Doctors doctors,HttpPostedFileBase Photo)
        {
            string image = "";
            string photoName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Photo.FileName;

            if (ModelState.IsValid)
            {
                image = Path.Combine(Server.MapPath("~/Uploads/Doctors"), photoName);
                Photo.SaveAs(image);
                doctors.Photo = photoName;
                db.Entry(doctors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameAz", doctors.DepartmentId);
            return View(doctors);
        }

        public ActionResult Delete(int Id)
        {
            Models.Doctors foundDoctor = db.Doctors.FirstOrDefault(d => d.Id == Id);

            if (foundDoctor != null)
            {
                db.Doctors.Remove(foundDoctor);
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
