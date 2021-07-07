using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            decimal quote = 50;
            var todayYear = DateTime.Now.Year;

            if (todayYear - insuree.DateOfBirth.Year < 18)
            {
                quote = quote + 50;
            }
            if (todayYear - insuree.DateOfBirth.Year < 25)
            {
                quote = quote + 50;
            }
            else if (todayYear - insuree.DateOfBirth.Year > 24)
            {
                quote = quote + 25;
            }
            if (insuree.CarYear < 2000)
            {
                quote += 25;
            }
            if (insuree.CarYear > 2015)
            {
                quote += 25;
            }

            if (insuree.CarMake.ToLower().Contains("porsche"))
            {
                quote += 25;
                if (insuree.CarModel.ToLower().Contains("911 carerra"))
                {
                    quote += 25;
                }
            }

            if (insuree.SpeedingTickets > 0)
            {
                quote = insuree.SpeedingTickets * 10 + quote;
            }

            if (insuree.DUI == true)
            {
                quote = quote * .25M + quote;
            }

            if (insuree.CoverageType == true)
            {
                quote = quote * .5M + quote;
            }
            insuree.Quote = quote;

            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                decimal quote = 50;
                var todayYear = DateTime.Now.Year;

                if (todayYear - insuree.DateOfBirth.Year < 18)
                {
                    quote = quote + 50;
                }
                if (todayYear - insuree.DateOfBirth.Year < 25)
                {
                    quote = quote + 50;
                }
                else if (todayYear - insuree.DateOfBirth.Year > 24)
                {
                    quote = quote + 25;
                }
                if (insuree.CarYear < 2000)
                {
                    quote += 25;
                }
                if (insuree.CarYear > 2015)
                {
                    quote += 25;
                }

                if (insuree.CarMake.ToLower().Contains("porsche"))
                {
                    quote += 25;
                    if (insuree.CarModel.ToLower().Contains("911 carerra"))
                    {
                        quote += 25;
                    }
                }

                if (insuree.SpeedingTickets > 0)
                {
                    quote = insuree.SpeedingTickets * 10 + quote;
                }

                if (insuree.DUI == true)
                {
                    quote = quote * .25M + quote;
                }

                if (insuree.CoverageType == true)
                {
                    quote = quote * .5M + quote;
                }
                insuree.Quote = quote;
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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