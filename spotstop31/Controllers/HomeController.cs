using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Device.Location;
using System.Data.SqlClient;
using System.Data;

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

        public ActionResult NewPosting (string Name, double myLat, double myLong, double rate)
        {
            String query = "INSERT INTO dbo.Rentees (Name, Latitude, Longitude, Rate) VALUES (@Name, @Latitude, @Longitude, @Rate)";
            using (SqlConnection connection = new SqlConnection("Server = tcp:mcos3q7bi2.database.windows.net,1433; Database = newspotstopdb; User ID = spotstopdb@mcos3q7bi2; Password = HackTX2015!; Trusted_Connection = False; Encrypt = True; Connection Timeout = 30"))
            using (SqlCommand command = new SqlCommand(query, connection))
            {    
                command.Parameters.Add("@Name", SqlDbType.NChar).Value = Name;

                command.Parameters.Add("@Latitude", SqlDbType.Float).Value = myLat;

                command.Parameters.Add("@Longitude", SqlDbType.Float).Value = myLong;

                command.Parameters.Add("@Rate", SqlDbType.Float).Value = rate;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return null;
        }

        public ActionResult NewSearch (double myLat, double myLong, int radius, double rate)
        {

            Random random = new Random();
            List<spotstop31.Models.SearchQuery> s = new List<spotstop31.Models.SearchQuery>();
            int amountOfSpots = 0;
            String query = "INSERT INTO dbo.Rentees (Name, Latitude, Longitude, [Available Spots]) VALUES ('Testcase', 0.000, 0.000, 2)";
            using (SqlConnection connection = new SqlConnection("Server = tcp:mcos3q7bi2.database.windows.net,1433; Database = newspotstopdb; User ID = spotstopdb@mcos3q7bi2; Password = HackTX2015!; Trusted_Connection = False; Encrypt = True; Connection Timeout = 30"))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
                //int radiusCoord = radius;

                GeoCoordinate coords = new GeoCoordinate(myLat, myLong);

            for (int i = 0; i < 100; i++)
            {
                double myLatitude = myLat;
                double myLongitude = myLong;
                spotstop31.Models.SearchQuery sq = new Models.SearchQuery();
                double randomNumberLat = 0.000000;
                double randomNumberLong = 0.000000;
                double randomSign = random.NextDouble();
                //double magnitudeLat = 0;
                //double magnitudeLong = 0;
                //bool latSignChange = false;
                //bool longSignChange = false;

                //if (randomSign < 0.5)
                //{
                //    randomNumberLat = random.NextDouble() * (-90);
                //    randomNumberLong = random.NextDouble() * (-180);
                //} else
                //{
                //    randomNumberLat = random.NextDouble() * (90);
                //    randomNumberLong = random.NextDouble() * (180);
                //}

                randomNumberLat = random.NextDouble() * (30.563148 - 29.901080) + 29.901080;
                randomNumberLong = -(random.NextDouble() * (98.216400 - 97.31521) + 97.31521);

                //randomNumberLat = random.NextDouble() * (radius * 2);
                //randomNumberLong = -(random.NextDouble() * (radius * 2);


                GeoCoordinate randomCoords = new GeoCoordinate(randomNumberLat, randomNumberLong);
                double magnitude = coords.GetDistanceTo(randomCoords); // calculutes distance from user coords to random coords in meters
                magnitude = magnitude * 0.00062137; // converting from meters to miles

                //if (Math.Sign(myLat) < 0 && Math.Sign(randomNumberLat) >= 0)
                //{
                //    magnitudeLat = Math.Abs(myLat) + Math.Abs(randomNumberLat);
                //}
                //else if(Math.Sign(myLat) > 0 && Math.Sign(randomNumberLat) >= 0)  {
                //    magnitudeLat = Math.Abs(myLat) + Math.Abs(randomNumberLat);
                //}

                //if (Math.Sign(myLong) < 0 && Math.Sign(randomNumberLong) >= 0)
                //{
                //    magnitudeLong = Math.Abs(myLong) + Math.Abs(randomNumberLong);
                //}
                //else if (Math.Sign(myLong) > 0 && Math.Sign(randomNumberLong) >= 0)
                //{
                //    magnitudeLong = Math.Abs(myLong) + Math.Abs(randomNumberLong);
                //}

                //if (!latSignChange)
                //{
                //    magnitudeLat = Math.Abs(randomNumberLat - myLat);
                //}
                //if (!longSignChange)
                //{
                //    magnitudeLong = Math.Abs(randomNumberLong - myLong);
                //}
                //double distance = Math.Sqrt(magnitudeLong * magnitudeLong + magnitudeLat * magnitudeLat);
                if (magnitude > radius)
                {
                    continue;
                }
                else
                {
                    amountOfSpots++;
                    sq.amountOfSpots = amountOfSpots;
                    sq.latitude = randomNumberLat;
                    sq.longitude = randomNumberLong;
                    sq.startTime = new DateTime(2015, 9, 25);
                    sq.endTime = new DateTime(2015, 9, 26);
                    sq.radius = radius;
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