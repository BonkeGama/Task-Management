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
    public class Beneficiary_SignatureController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Beneficiary_Signature
        public ActionResult Index()
        {
            var beneficiary_Signatures = db.beneficiary_Signatures.Include(b => b.Order);
            return View(beneficiary_Signatures.ToList());
        }

        // GET: Beneficiary_Signature/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary_Signature beneficiary_Signature = db.beneficiary_Signatures.Find(id);
            if (beneficiary_Signature == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary_Signature);
        }

        // GET: Beneficiary_Signature/Create
        public ActionResult Create(int id)
        {
           Beneficiary_Signature beneficiary_Signature = new Beneficiary_Signature();
           beneficiary_Signature.Order_ID = id;
            ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Tasker_ID");
            return View(beneficiary_Signature);
        }

        // POST: Beneficiary_Signature/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Signaturee_ID,Sign_Date,MySignature,SignedBy,Order_ID")] Beneficiary_Signature beneficiary_Signature)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order();
                beneficiary_Signature.Sign_Date = DateTime.Now;
                db.beneficiary_Signatures.Add(beneficiary_Signature);
                db.SaveChanges();
                order.UpdateStatus(beneficiary_Signature.Order_ID, "Arrived for service rendering");
                return RedirectToAction("Index","Home");
            }


            ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Tasker_ID", beneficiary_Signature.Order_ID);
            return View(beneficiary_Signature);
        }

        // GET: Beneficiary_Signature/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary_Signature beneficiary_Signature = db.beneficiary_Signatures.Find(id);
            if (beneficiary_Signature == null)
            {
                return HttpNotFound();
            }
            ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Tasker_ID", beneficiary_Signature.Order_ID);
            return View(beneficiary_Signature);
        }

        // POST: Beneficiary_Signature/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Signaturee_ID,Sign_Date,MySignature,SignedBy,Order_ID")] Beneficiary_Signature beneficiary_Signature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiary_Signature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Tasker_ID", beneficiary_Signature.Order_ID);
            return View(beneficiary_Signature);
        }

        // GET: Beneficiary_Signature/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficiary_Signature beneficiary_Signature = db.beneficiary_Signatures.Find(id);
            if (beneficiary_Signature == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary_Signature);
        }

        // POST: Beneficiary_Signature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beneficiary_Signature beneficiary_Signature = db.beneficiary_Signatures.Find(id);
            db.beneficiary_Signatures.Remove(beneficiary_Signature);
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
