using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enlighten.Models
{
    public class LessonAttachment
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string FileType { get; set; }

        public byte[] Data { get; set; }

        [Required]
        public Lesson Lesson { get; set; }
    }
}