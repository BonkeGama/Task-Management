using Microsoft.AspNet.Identity;
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
    public class DriverWalletsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DriverWallets
        public ActionResult Index()
        {
            var uid = User.Identity.GetUserId();
            string id = uid;
            var driverWallets = db.DriverWallets.Include(d => d.Driver);
            return View(driverWallets.ToList());
        }

        // GET: DriverWallets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverWallets driverWallets = db.DriverWallets.Find(id);
            if (driverWallets == null)
            {
                return HttpNotFound();
            }
            return View(driverWallets);
        }

        // GET: DriverWallets/Create
        public ActionResult Create()
        {
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_Name");
            return View();
        }

        // POST: DriverWallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DWallet_ID,Driver_ID,DAmount")] DriverWallets driverWallets)
        {
     
       
                db.DriverWallets.Add(driverWallets);

            driverWallets.DAmount += 100;
            driverWallets.CalcCommission();
                db.SaveChanges();
                return RedirectToAction("Index");
            

            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_Name", driverWallets.Driver_ID);
            return View(driverWallets);
        }

        // GET: DriverWallets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverWallets driverWallets = db.DriverWallets.Find(id);
            if (driverWallets == null)
            {
                return HttpNotFound();
            }
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_Name", driverWallets.Driver_ID);
            return View(driverWallets);
        }

        // POST: DriverWallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DWallet_ID,Driver_ID,DAmount")] DriverWallets driverWallets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driverWallets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_Name", driverWallets.Driver_ID);
            return View(driverWallets);
        }

        // GET: DriverWallets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverWallets driverWallets = db.DriverWallets.Find(id);
            if (driverWallets == null)
            {
                return HttpNotFound();
            }
            return View(driverWallets);
        }

        // POST: DriverWallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            DriverWallets driverWallets = db.DriverWallets.Find(id);
            db.DriverWallets.Remove(driverWallets);
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
