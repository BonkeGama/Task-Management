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
    public class SignaturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Signatures
        public ActionResult Index()
        {
            var signatures = db.Signatures.Include(s => s.ToolSet);
            return View(signatures.ToList());
        }

        // GET: Signatures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signature signature = db.Signatures.Find(id);
            if (signature == null)
            {
                return HttpNotFound();
            }
            return View(signature);
        }

        // GET: Signatures/Create
        public ActionResult Create(int id)
        {
            Signature signature = new Signature();
            signature.Tool_ID = id;
            ViewBag.Tool_ID = new SelectList(db.ToolSets, "Tool_ID", "Tool_SetName");
            return View(signature);
        }

        // POST: Signatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Signature_ID,MySignature,Tool_ID")] Signature signature)
        {
            if (ModelState.IsValid)
            {
                db.Signatures.Add(signature);
                db.SaveChanges();
                ToolSet toolSet = db.ToolSets.Find(signature.Tool_ID);
                toolSet.availability = true;
                db.Entry(toolSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Borrow","ToolSets");
            }

            ViewBag.Tool_ID = new SelectList(db.ToolSets, "Tool_ID", "Tool_SetName", signature.Tool_ID);
            return View(signature);
        }

        // GET: Signatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signature signature = db.Signatures.Find(id);
            if (signature == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tool_ID = new SelectList(db.ToolSets, "Tool_ID", "Tool_SetName", signature.Tool_ID);
            return View(signature);
        }

        // POST: Signatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Signature_ID,MySignature,Tool_ID")] Signature signature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(signature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tool_ID = new SelectList(db.ToolSets, "Tool_ID", "Tool_SetName", signature.Tool_ID);
            return View(signature);
        }

        // GET: Signatures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signature signature = db.Signatures.Find(id);
            if (signature == null)
            {
                return HttpNotFound();
            }
            return View(signature);
        }

        // POST: Signatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Signature signature = db.Signatures.Find(id);
            db.Signatures.Remove(signature);
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
