using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Enlighten.Models;

public partial class Manage : System.Web.UI.Page
{
    public Member member;

    protected void Page_Load(object sender, EventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        member = dbContext.Members.Where(m => m.Email == Context.User.Identity.Name).FirstOrDefault();
    }
}