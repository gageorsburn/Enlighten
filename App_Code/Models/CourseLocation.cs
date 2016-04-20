using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enlighten.Models
{
    public class CourseLocation
    {
        public int Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string BuildingName { get; set; }
    }
}