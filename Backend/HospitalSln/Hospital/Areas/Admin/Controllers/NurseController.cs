using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital.Models;

namespace Hospital.Areas.Admin.Controllers
{
    public class NurseController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Admin/Nurse
        public ActionResult Index()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                var nurses = db.Nurses.Include(n => n.Departments).Include(n => n.Doctors);
                return View(nurses.ToList());
            }
            return RedirectToAction("Index", "Login");
        }


        // GET: Admin/Nurse/Create
        public ActionResult Create()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameEng");
                ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name");
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Nurse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Phone,Address,WorkHours,Salary,DoctorId,DepartmentId")] Nurses nurses)
        {
            if (ModelState.IsValid)
            {
                db.Nurses.Add(nurses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameEng", nurses.DepartmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", nurses.DoctorId);
            return View(nurses);
        }

        // GET: Admin/Nurse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nurses nurses = db.Nurses.Find(id);
                if (nurses == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameAz", nurses.DepartmentId);
                ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", nurses.DoctorId);
                return View(nurses);
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Nurse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Phone,Address,WorkHours,Salary,DoctorId,DepartmentId")] Nurses nurses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nurses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "NameAz", nurses.DepartmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", nurses.DoctorId);
            return View(nurses);
        }

        public ActionResult Delete(int Id)
        {
            Models.Nurses foundNurse = db.Nurses.FirstOrDefault(n => n.Id == Id);

            if (foundNurse != null)
            {
                db.Nurses.Remove(foundNurse);
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
