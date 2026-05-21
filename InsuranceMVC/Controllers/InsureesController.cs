using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InsuranceMVC.Models;

namespace InsuranceMVC.Controllers
{
    public class InsureesController : Controller
    {
        private InsuranceMVCEntities db = new InsuranceMVCEntities();

        // GET: Insurees
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insurees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insurees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insurees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InsureeId,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,FullCoverage,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                // حساب العمر
                int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                if (insuree.DateOfBirth > DateTime.Now.AddYears(-age)) age--;

                decimal quote = 50m;

                // العمر
                if (age <= 18)
                    quote += 100m;
                else if (age >= 19 && age <= 25)
                    quote += 50m;
                else
                    quote += 25m;

                // سنة السيارة
                if (insuree.CarYear < 2000)
                    quote += 25m;
                if (insuree.CarYear > 2015)
                    quote += 25m;

                // ماركة السيارة
                if (insuree.CarMake.ToLower() == "porsche")
                {
                    quote += 25m;
                    if (insuree.CarModel.ToLower() == "911 carrera")
                        quote += 25m;
                }

                // تذاكر السرعة
                quote += insuree.SpeedingTickets * 10m;

                // DUI
                if (insuree.DUI)
                    quote *= 1.25m;

                // Full Coverage
                if (insuree.FullCoverage)
                    quote *= 1.50m;

                insuree.Quote = quote;

                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insurees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insurees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InsureeId,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,FullCoverage,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insurees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insurees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Insurees/Admin
        public ActionResult Admin()
        {
            return View(db.Insurees.ToList());
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