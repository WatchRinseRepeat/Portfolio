using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Rent.Models;
using TheatreCMS3.Controllers;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Rent.Controllers
{
    
    public class RentalHistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent/RentalHistories
        public ActionResult Index()
        {
            return View(db.RentalHistories.ToList());
        }

        // GET: Rent/RentalHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // GET: Rent/RentalHistories/Create
        [HistoryManagerAuthorize(Roles ="HistoryManager")]
        public ActionResult Create()
        {
            IEnumerable<Rental> rentals = db.Rentals.ToList();
            ViewBag.data = rentals;
            return View();
        }

        // POST: Rent/RentalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalHistoryId,RentalDamaged,DamagesIncurred,RentalHistoryID,SelectedRentalId")] RentalHistory rentalHistory)
        {
            rentalHistory.Rental = db.Rentals.Find(rentalHistory.SelectedRentalId);

            if (ModelState.IsValid)
            {
                db.RentalHistories.Add(rentalHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentalHistory);
        }

        // GET: Rent/RentalHistories/Edit/5
        [HistoryManagerAuthorize(Roles = "HistoryManager")]
        public ActionResult Edit(int? id)
        {
            IEnumerable<Rental> rentals = db.Rentals.ToList();
            ViewBag.data = rentals;
            ViewBag.selectedRentalId = db.RentalHistories.Find(id).SelectedRentalId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // POST: Rent/RentalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalHistoryId,RentalDamaged,DamagesIncurred,SelectedRentalId")] RentalHistory rentalHistory)
        {
            
            
            if (ModelState.IsValid)
            {
                var rentalHistoryObject = db.RentalHistories.Find(rentalHistory.RentalHistoryId);
                rentalHistoryObject.SelectedRentalId = rentalHistory.SelectedRentalId;
                rentalHistoryObject.Rental = db.Rentals.Find(rentalHistory.SelectedRentalId);
                rentalHistoryObject.DamagesIncurred = rentalHistory.DamagesIncurred;
                rentalHistoryObject.RentalDamaged = rentalHistory.RentalDamaged;

                db.Entry(rentalHistoryObject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.data = db.Rentals.ToList();
            ViewBag.selectedRentalId = rentalHistory.SelectedRentalId;
            return View(rentalHistory);
        }

        // GET: Rent/RentalHistories/Delete/5
        [HistoryManagerAuthorize(Roles = "HistoryManager")]
        public ActionResult Delete(int? id)
        {           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            if (rentalHistory == null)
            {
                return HttpNotFound();
            }
            return View(rentalHistory);
        }

        // POST: Rent/RentalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalHistory rentalHistory = db.RentalHistories.Find(id);
            db.RentalHistories.Remove(rentalHistory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AccessDenied()
        {
            return View();
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
