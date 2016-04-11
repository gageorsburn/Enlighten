using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Enlighten.Models;
using Microsoft.AspNet.Identity;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.IsAuthenticated)
            Response.Redirect("~/Default.aspx");
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        string HashedPassword = new PasswordHasher().HashPassword(Password.Text); 

        Member member = new Member()
        {
            FirstName = FirstName.Text,
            LastName = LastName.Text,
            Email = Email.Text,
            Password = HashedPassword
        };

        ApplicationDbContext dbContext = new ApplicationDbContext();

        dbContext.Members.Add(member);

        dbContext.SaveChanges();

        Response.Redirect("~/Default.aspx");
    }
}