using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace spotstop31.Models
{
    public class SearchReturn
    {
        public double latitude = 30.2848691d;
        public double longitude = -97.7364655d;
        public double radius;
        public int startTime;
        public int endTime;
        public int amountOfSpots;
        public string specialInstructions;
    }

    public class SearchQuery
    {
        public double latitude = 30.2848691d;
        public double longitude = -97.7364655d;
        public double radius;
        public DateTime startTime;
        public DateTime endTime;
        public int amountOfSpots;
    }


}