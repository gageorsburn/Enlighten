using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Enlighten.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<LessonAttachment> LessonAttachments { get; set; }
        public DbSet<CourseArticle> CourseArticles { get; set; }
        public DbSet<CourseUrl> CourseUrls { get; set; }
    }
}