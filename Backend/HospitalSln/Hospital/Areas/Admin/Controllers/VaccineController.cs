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
    public class VaccineController : Controller
    {
        private HospitalEntities db = new HospitalEntities();

        // GET: Admin/Vaccine
        public ActionResult Index()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                return View(db.Vaccines.ToList());
            }
            return RedirectToAction("Index", "Login");
        }



        // GET: Admin/Vaccine/Create
        public ActionResult Create()
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Vaccine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Vaccines vaccines)
        {
            if (ModelState.IsValid)
            {
                db.Vaccines.Add(vaccines);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaccines);
        }

        // GET: Admin/Vaccine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["isLogin"] != null && (bool)Session["isLogin"] == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vaccines vaccines = db.Vaccines.Find(id);
                if (vaccines == null)
                {
                    return HttpNotFound();
                }
                return View(vaccines);
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Vaccine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Vaccines vaccines)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaccines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaccines);
        }

        public ActionResult Delete(int Id)
        {
            Models.Vaccines foundVaccine = db.Vaccines.FirstOrDefault(v => v.Id == Id);

            if (foundVaccine != null)
            {
                db.Vaccines.Remove(foundVaccine);
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
