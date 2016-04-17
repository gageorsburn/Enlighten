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
        /* If the user is already logged in, redirect to the home page. */
        if(Request.IsAuthenticated)
            Response.Redirect("~/Default.aspx");
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        /* Hash password so it isn't stored in plain text. */
        string HashedPassword = new PasswordHasher().HashPassword(Password.Text); 

        /* Create a new member object to store in the database. */
        Member member = new Member()
        {
            FirstName = FirstName.Text,
            LastName = LastName.Text,
            Email = Email.Text,
            Password = HashedPassword
        };

        /* Create the database connection. */
        ApplicationDbContext dbContext = new ApplicationDbContext();

        /* Add the new member to the database. */
        dbContext.Members.Add(member);

        /* Save changes to the database. */
        dbContext.SaveChanges();

        /* If we make it to this point the user has registered successfully. */
        SuccessLabel.ForeColor = System.Drawing.Color.Green;
        SuccessLabel.Text = "Registered successfully!";

        Response.AddHeader("REFRESH", "5;URL=Default.aspx");
    }
}