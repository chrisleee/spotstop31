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

        public ActionResult NewSearch (double location, int radius)
        {
            Random random = new Random();
            double distancemagnitude = 0;
            List<spotstop31.Models.SearchQuery> s = new List<spotstop31.Models.SearchQuery>();

            for (int i = 0; i < 100; i++)
            {
                spotstop31.Models.SearchQuery sq = new Models.SearchQuery();
                double randomNumberLat = 0.000000;
                double randomNumberLong = 0.000000;
                randomNumberLat = random.NextDouble() * (100);
                randomNumberLong = random.NextDouble() * (100);
                distancemagnitude = randomNumberLat * randomNumberLat + randomNumberLong * randomNumberLong;
                distancemagnitude = Math.Sqrt(distancemagnitude);

                if (distancemagnitude > radius)
                {
                    continue;
                }
                else
                {
                    sq.latitude = randomNumberLat;
                    sq.longitude = randomNumberLong;
                    sq.startTime = new DateTime(2015, 9, 25);
                    sq.endTime = new DateTime(2015, 9, 26);
                    sq.radius = 30.00;
                    s.Add(sq);
                }
            }

            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info()
        {
            return Content("Data");
        }
    }
}