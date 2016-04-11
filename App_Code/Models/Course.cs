using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enlighten.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Section { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }

        public int ProfessorId { get; set; }
        public int AssistantId { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Member> Students { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }    
    }
}