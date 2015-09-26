using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace spotstop31.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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

        public ActionResult SomeActionMethod()
        {
            spotstop31.Models.SearchQuery sq = new Models.SearchQuery();

            sq.latitude = 50.000;
            sq.longitude = 60.000;
            sq.startTime = new DateTime(2015, 9, 25);
            sq.endTime = new DateTime(2015, 9, 26);
            sq.radius = 30.00;

            Random random = new Random();

            for (int i=0; i<100; i++)
            {
                
                double randomNumberLat = 0.000000;
                double randomNumberLong = 0.000000;
                randomNumberLat += random.Next(-100, 100);
                randomNumberLong += random.Next(-100, 100);

            }
            return Json(sq, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info()
        {
            return Content("Data");
        }
    }
}