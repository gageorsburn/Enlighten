using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Enlighten.Models
{
    public class Submission
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public string Title { get; set; }
        public string FileType { get; set; }

        public byte[] Data { get; set; }

        [Required]
        public Member Member { get; set; }

        [Required]
        public Assignment Assignment { get; set; }

        [Required]
        public Course Course { get; set; }
    }
}