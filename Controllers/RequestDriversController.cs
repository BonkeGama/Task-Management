using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskA.Models;

namespace TaskA.Controllers
{
    public class RequestDriversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequestDrivers
        public ActionResult Index()
        {
            var requestDrivers = db.RequestDrivers.Include(r => r.Driver);
            return View(requestDrivers.ToList());
        }

        // GET: RequestDrivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestDriver requestDriver = db.RequestDrivers.Find(id);
            if (requestDriver == null)
            {
                return HttpNotFound();
            }
            return View(requestDriver);
        }

        // GET: RequestDrivers/Create
        public ActionResult Create()
        {
            RequestDriver requestDriver = new RequestDriver();
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_Name");
            return View();
        }

        // POST: RequestDrivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RDriver_ID,Driver_ID,TaskerFullName,TaskerCellNo,FromLocation,ToLocation,RequestDate")] RequestDriver requestDriver)
        {
            if (ModelState.IsValid)
            {
                db.RequestDrivers.Add(requestDriver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_Name", requestDriver.Driver_ID);
            return View(requestDriver);
        }

        // GET: RequestDrivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestDriver requestDriver = db.RequestDrivers.Find(id);
            if (requestDriver == null)
            {
                return HttpNotFound();
            }
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_Name", requestDriver.Driver_ID);
            return View(requestDriver);
        }

        // POST: RequestDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RDriver_ID,Driver_ID,TaskerFullName,TaskerCellNo,FromLocation,ToLocation,RequestDate")] RequestDriver requestDriver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestDriver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_Name", requestDriver.Driver_ID);
            return View(requestDriver);
        }

        // GET: RequestDrivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestDriver requestDriver = db.RequestDrivers.Find(id);
            if (requestDriver == null)
            {
                return HttpNotFound();
            }
            return View(requestDriver);
        }

        // POST: RequestDrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestDriver requestDriver = db.RequestDrivers.Find(id);
            db.RequestDrivers.Remove(requestDriver);
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
