using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Enlighten.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public Course Course;
    }
}