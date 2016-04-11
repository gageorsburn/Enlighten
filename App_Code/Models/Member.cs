using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enlighten.Models
{
    public class Member : Microsoft.AspNet.Identity.IUser
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public byte[] Picture { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        #region Identity
        [NotMapped]
        string IUser<string>.Id
        {
            get
            {
                return Id.ToString();
            }
        }

        [NotMapped]
        public string UserName
        {
            get
            {
                return Email;
            }
            set { }
        }
        #endregion
    }
}