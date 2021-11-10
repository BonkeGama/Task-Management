using TaskA.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskA.Controllers
{
    public class SpinsController : Controller
    {
        // GET: Spins
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Win()
        {
            var uid = User.Identity.GetUserId();

            var check = (from i in db.Spins
                         where i.Profile_ID == uid
                         select i.Profile_ID).SingleOrDefault();
            if (check == null)
            {
                Spins spins = new Spins();
                spins.Profile_ID = uid;
                spins.No_Spins = 0;
                db.Spins.Add(spins);
                db.SaveChanges();
            }
     

            ModelState.Clear();
            Spins spin = (from b in db.Spins
                          where b.Profile_ID == uid
                          select b).FirstOrDefault();

            var list = (from c in db.Coupons
                        where c.Profile_ID == uid && c.Coupon_Status != "Used"
                        select c).ToList();

            ViewBag.SpinsLeft = spin.No_Spins;

            return View(list);
        }

        [HttpPost]
        public ActionResult Win(Coupon coupon)
        {
           
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[4];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            var uid = User.Identity.GetUserId();
            coupon.Coupon_Code = coupon.Coupon_Value + "RandOFF" + finalString;
            coupon.Profile_ID = User.Identity.GetUserId();


            var spin = (from b in db.Spins
                        where b.Profile_ID == uid
                        select b).FirstOrDefault();

            spin.No_Spins -= 1;

            ViewBag.SpinsLeft = spin.No_Spins;
            ViewBag.CouponCode = coupon.Coupon_Code;
            TempData["Coupons"] = "TEST";
            db.Entry(spin).State = EntityState.Modified;
            db.Coupons.Add(coupon);
            db.SaveChanges();


            var emails = db.Clients.Where(c => c.Profile_ID == uid).Select(c => c.Profile_Email).FirstOrDefault();

            var name = db.Clients.Where(c => c.Profile_Name == uid).Select(c => c.Profile_Name).FirstOrDefault();
            var lastName = db.Clients.Where(c => c.Profile_Surname == uid).Select(c => c.Profile_Surname).FirstOrDefault();


            // send  emails and redirect to 
            #region MailBooking

            string body = "Dear " + name + " " + lastName + "<br/>"
                + "<br/>"
                + "<h2>Congratulations You Have Won A R" + coupon.Coupon_Value + " Coupon</h2>"

                + "<br/>"
                + "<br/>"
                + "<h3>Your Coupon Code Is: " +
                coupon.Coupon_Code +
                "</h3>" +


                "<br/>" +
                "<br/>" +

                "You may use it at checkout to recieve a discount of the specified value." +
                 "<br/>" +
                "<br/>" +
                "Regards," +
                "<br/>" +
                "Messenger Kings Courier Management";


                 ViewBag.Body = $"Dear " + name + " " + lastName + "<br/>" +
              $"Congratulations You Have Won A R" + coupon.Coupon_Value + " Coupon" +
              $"<br/>" +
              $"<br/>" +
               $" Coupon Code Is: " +coupon.Coupon_Code +""+
              $"<br/>" +
              $"You may use it at checkout to recieve a discount of the specified value." +
              $"<br/>" +
              $"Task A";
            Email email = new Email();
            email.Gmail("Rewards", ViewBag.Body, emails);
            #endregion

            return View(coupon);
        }
    }
}