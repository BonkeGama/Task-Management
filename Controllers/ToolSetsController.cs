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
    public class ToolSetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private string GenerateQRCode(string qrcodeText)
        {
            string folderPath = "~/Images/";
            string imagePath = "~/Images/QrCode.jpg";
            // create new Directory if not exist
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return imagePath;
        }
        // GET: ToolSets

        public ActionResult Read()
        {
            return View(ReadQRCode());
        }

        private QRCode ReadQRCode()
        {
            QRCode barcodeModel = new QRCode();
            string barcodeText = "";
            string imagePath = "~/Images/QrCode.jpg";
            string barcodePath = Server.MapPath(imagePath);
            var barcodeReader = new BarcodeReader();
            //Decode the image to text
            var result = barcodeReader.Decode(new Bitmap(barcodePath));
            if (result != null)
            {
                barcodeText = result.Text;
            }
            return new QRCode() { QRCodeText = barcodeText, QRCodeImagePath = imagePath };
        }

        public ActionResult Index()
        {
            var toolSets = db.ToolSets.Include(t => t.ServiceCat);
            return View(toolSets.ToList());
        }
        public ActionResult Borrow()
        {
            var toolSets = db.ToolSets.Include(t => t.ServiceCat);
            return View(toolSets.ToList());
        }

        // GET: ToolSets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolSet toolSet = db.ToolSets.Find(id);
            if (toolSet == null)
            {
                return HttpNotFound();
            }
            return View(toolSet);
        }

        // GET: ToolSets/Create
        public ActionResult Create()
        {
            ViewBag.ServiceCat_ID = new SelectList(db.ServiceCats, "ServiceCat_ID", "Category");
            return View();
        }

        // POST: ToolSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tool_ID,Tool_SetName,ToolImage,QRCodeText,QRCodeImagePath,availability,ServiceCat_ID")] ToolSet toolSet, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {
                if (filelist != null && filelist.ContentLength > 0)
                {
                    toolSet.ToolImage = ConvertToBytes(filelist);
                }
                toolSet.availability = true;
                toolSet.QRCodeText = "https://TaskA20211110094820.azurewebsites.net/Signatures/Create/" + toolSet.Tool_ID;
                toolSet.QRCodeImagePath = GenerateQRCode(toolSet.QRCodeText);
                db.ToolSets.Add(toolSet);
                db.SaveChanges();
                ToolSet tool = db.ToolSets.Find(toolSet.Tool_ID);
                tool.QRCodeText = "https://TaskA20211110094820.azurewebsites.net/Signatures/Create/" + tool.Tool_ID;
                tool.QRCodeImagePath = GenerateQRCode(tool.QRCodeText);
                db.Entry(tool).State = EntityState.Modified;
                return RedirectToAction("Index");
            }

            ViewBag.ServiceCat_ID = new SelectList(db.ServiceCats, "ServiceCat_ID", "Category", toolSet.ServiceCat_ID);
            return View(toolSet);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }


        // GET: ToolSets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolSet toolSet = db.ToolSets.Find(id);
            if (toolSet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceCat_ID = new SelectList(db.ServiceCats, "ServiceCat_ID", "Category", toolSet.ServiceCat_ID);
            return View(toolSet);
        }

        // POST: ToolSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tool_ID,Tool_SetName,ToolImage,QRCodeText,QRCodeImagePath,availability,ServiceCat_ID")] ToolSet toolSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toolSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceCat_ID = new SelectList(db.ServiceCats, "ServiceCat_ID", "Category", toolSet.ServiceCat_ID);
            return View(toolSet);
        }

        // GET: ToolSets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolSet toolSet = db.ToolSets.Find(id);
            if (toolSet == null)
            {
                return HttpNotFound();
            }
            return View(toolSet);
        }

        // POST: ToolSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToolSet toolSet = db.ToolSets.Find(id);
            db.ToolSets.Remove(toolSet);
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
