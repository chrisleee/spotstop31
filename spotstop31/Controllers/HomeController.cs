using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Device.Location;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

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

        public List<PosterData> SerializingDatabase()
        {
            var listOfPosterData = new List<PosterData>();
            String query = "SELECT * FROM dbo.Rentees";
            //String query = "INSERT INTO dbo.Rentees (Name, Latitude, Longitude, [Available Spots]) VALUES ('Testcase', 0.000, 0.000, 2)";
            using (SqlConnection connection = new SqlConnection("Server = tcp:mcos3q7bi2.database.windows.net,1433; Database = newspotstopdb; User ID = spotstopdb@mcos3q7bi2; Password = HackTX2015!; Trusted_Connection = False; Encrypt = True; Connection Timeout = 30"))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                // Use the above SqlCommand object to create a SqlDataReader object.
                using (SqlDataReader queryCommandReader = command.ExecuteReader())
                {
                    while (queryCommandReader.Read())
                    {
                        PosterData posterData = new PosterData();
                        posterData.latitude = queryCommandReader.GetDouble(queryCommandReader.GetOrdinal("Latitude"));
                        posterData.longitude = queryCommandReader.GetDouble(queryCommandReader.GetOrdinal("Longitude"));
                        posterData.rate = queryCommandReader.GetDouble(queryCommandReader.GetOrdinal("Rate"));
                        posterData.name = queryCommandReader.GetString(queryCommandReader.GetOrdinal("Name"));
                        listOfPosterData.Add(posterData);
                    }
                }

                command.ExecuteNonQuery();
                connection.Close();
                return listOfPosterData;
            }
            
        }

        public ActionResult NewSearch (double myLat, double myLong, double radius, double rate)
        {

            List<spotstop31.Models.SearchQuery> s = new List<spotstop31.Models.SearchQuery>();
            int amountOfSpots = 0;
            GeoCoordinate coords = new GeoCoordinate(myLat, myLong);

                var fetchedPosterData = new List<PosterData>();
                fetchedPosterData = SerializingDatabase();

                for (int i=0; i<fetchedPosterData.Count; i++)
                {
                    spotstop31.Models.SearchQuery sq = new Models.SearchQuery();
                    GeoCoordinate posterCoords = new GeoCoordinate(fetchedPosterData[i].latitude, fetchedPosterData[i].longitude);
                    double magnitude = coords.GetDistanceTo(posterCoords); // calculutes distance from user coords to random coords in meters
                    magnitude = magnitude*0.00062137; // converting from meters to miles

                    if (magnitude < radius)
                    {
                        amountOfSpots++;
                        sq.amountOfSpots = amountOfSpots;
                        sq.latitude = fetchedPosterData[i].latitude;
                        sq.longitude = fetchedPosterData[i].longitude;
                        sq.startTime = new DateTime(2015, 9, 25);
                        sq.endTime = new DateTime(2015, 9, 26);
                        sq.radius = radius;
                        sq.name = fetchedPosterData[i].name;
                        sq.rate = fetchedPosterData[i].rate;
                        sq.distance = Math.Round(magnitude, 2);
                        s.Add(sq);
                    }
                }

            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info()
        {
            return Content("Data");
        }

        public ActionResult Intro() {
            return View();
        }
    }

    public class PosterData
    {
        public double longitude;
        public double latitude;
        public double rate;
        public string name;
    }
}