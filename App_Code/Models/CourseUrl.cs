using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enlighten.Models
{
    public class CourseUrl
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public Course Course { get; set; }
    }
}