using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enlighten.Models
{
    public class LessonAttachment
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string FileType { get; set; }

        public byte[] Data { get; set; }

        public Lesson Lesson { get; set; }
    }
}