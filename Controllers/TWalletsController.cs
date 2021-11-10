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
    public class TWalletsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TWallets
        public ActionResult Index()
        {
            var tWallets = db.TWallets.Include(t => t.Tasker);
            return View(tWallets.ToList());
        }

        // GET: TWallets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWallet tWallet = db.TWallets.Find(id);
            if (tWallet == null)
            {
                return HttpNotFound();
            }
            return View(tWallet);
        }

        // GET: TWallets/Create
        public ActionResult Create()
        {
            ViewBag.Tasker_ID = new SelectList(db.Taskers, "Tasker_ID", "Tasker_Name");
            return View();
        }

        // POST: TWallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TWallet_ID,Tasker_ID,TAmount")] TWallet tWallet)
        {
            if (ModelState.IsValid)
            {
                tWallet.TAmount += 100;
                db.TWallets.Add(tWallet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tasker_ID = new SelectList(db.Taskers, "Tasker_ID", "Tasker_Name", tWallet.Tasker_ID);
            return View(tWallet);
        }

        // GET: TWallets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWallet tWallet = db.TWallets.Find(id);
            if (tWallet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tasker_ID = new SelectList(db.Taskers, "Tasker_ID", "Tasker_Name", tWallet.Tasker_ID);
            return View(tWallet);
        }

        // POST: TWallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TWallet_ID,Tasker_ID,TAmount")] TWallet tWallet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tWallet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tasker_ID = new SelectList(db.Taskers, "Tasker_ID", "Tasker_Name", tWallet.Tasker_ID);
            return View(tWallet);
        }

        // GET: TWallets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TWallet tWallet = db.TWallets.Find(id);
            if (tWallet == null)
            {
                return HttpNotFound();
            }
            return View(tWallet);
        }

        // POST: TWallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TWallet tWallet = db.TWallets.Find(id);
            db.TWallets.Remove(tWallet);
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
