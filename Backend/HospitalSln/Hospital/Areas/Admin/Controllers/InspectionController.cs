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
    public class InspectionController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Admin/Inspection
        public ActionResult Index()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                var inspections = db.Inspections.Include(i => i.Doctors);
                return View(inspections.ToList());
            }
            return RedirectToAction("Index", "Login");
        }


        // GET: Admin/Inspection/Create
        public ActionResult Create()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name");
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Inspection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,DoctorId")] Inspections inspections)
        {
            if (ModelState.IsValid)
            {
                db.Inspections.Add(inspections);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", inspections.DoctorId);
            return View(inspections);
        }

        // GET: Admin/Inspection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Inspections inspections = db.Inspections.Find(id);
                if (inspections == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", inspections.DoctorId);
                return View(inspections);
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Inspection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,DoctorId")] Inspections inspections)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspections).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", inspections.DoctorId);
            return View(inspections);
        }

        public ActionResult Delete(int Id)
        {
            Models.Inspections foundInspection = db.Inspections.FirstOrDefault(i => i.Id == Id);

            if (foundInspection != null)
            {
                db.Inspections.Remove(foundInspection);
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
