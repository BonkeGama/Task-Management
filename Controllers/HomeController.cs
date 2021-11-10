using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskA.Models;

namespace TaskA.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{

            using (var context = new ApplicationDbContext())
            {
                ViewBag.Count = context.Bookings.SqlQuery(" SELECT * FROM dbo.Bookings").Count();
				ViewBag.Taskers = context.Taskers.SqlQuery(" SELECT * FROM dbo.Taskers").Count();
				ViewBag.Drivers = context.Drivers.SqlQuery(" SELECT * FROM dbo.Drivers").Count();
				ViewBag.Clients = context.Clients.SqlQuery(" SELECT * FROM dbo.Clients").Count();

			}

            return View();
		}
		public ActionResult HomePage()
		{
            return View();
		
		}
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}