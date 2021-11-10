using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskA.Models;
using ZXing;

namespace TaskA.Controllers
{
    public class BookToolsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: BookTools
        public ActionResult Index()
        {
            var bookTools = db.BookTools.Include(b => b.Tasker);
            return View(bookTools.ToList());
        }
        public ActionResult ReturnTool()
        {
            var uid = User.Identity.GetUserId();
            var bookTools = db.BookTools.Include(b => b.Tasker).Include(b=>b.ToolSet).Where(b=>b.Tasker_ID == uid && b.ToolSet.availability == false);
            return View(bookTools.ToList());
        }
        // GET: BookTools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTool bookTool = db.BookTools.Find(id);
            if (bookTool == null)
            {
                return HttpNotFound();
            }
            return View(bookTool);
        }

        // GET: BookTools/Create
        public ActionResult Create(int id)
        {
            BookTool bookTool = new BookTool();
            bookTool.Tool_ID = id;
           
            ViewBag.Tasker_ID = new SelectList(db.Taskers, "Tasker_ID", "Tasker_Name");
            return View(bookTool);
        }

        // POST: BookTools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookTool_ID,Tool_ID,Tasker_ID")] BookTool bookTool)
        {
            if (ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();
                bookTool.Tasker_ID = uid;
                db.BookTools.Add(bookTool);
                db.SaveChanges();
                ToolSet toolSet = db.ToolSets.Find(bookTool.Tool_ID);
                toolSet.availability = false;
                db.Entry(toolSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Borrow","ToolSets");
            }

            ViewBag.Tasker_ID = new SelectList(db.Taskers, "Tasker_ID", "Tasker_Name", bookTool.Tasker_ID);
            return View(bookTool);
        }

        // GET: BookTools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTool bookTool = db.BookTools.Find(id);
            if (bookTool == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tasker_ID = new SelectList(db.Taskers, "Tasker_ID", "Tasker_Name", bookTool.Tasker_ID);
            return View(bookTool);
        }

        // POST: BookTools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookTool_ID,Tool_ID,Tasker_ID")] BookTool bookTool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookTool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tasker_ID = new SelectList(db.Taskers, "Tasker_ID", "Tasker_Name", bookTool.Tasker_ID);
            return View(bookTool);
        }

        // GET: BookTools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTool bookTool = db.BookTools.Find(id);
            if (bookTool == null)
            {
                return HttpNotFound();
            }
            return View(bookTool);
        }

        // POST: BookTools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookTool bookTool = db.BookTools.Find(id);
            db.BookTools.Remove(bookTool);
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
