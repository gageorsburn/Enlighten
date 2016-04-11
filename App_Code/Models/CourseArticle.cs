using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enlighten.Models
{
    public class CourseArticle
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public Course Course { get; set; }
        public Member Author { get; set; }
    }
}