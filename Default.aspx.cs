using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Enlighten;
using Enlighten.Models;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.IsAuthenticated)
        {
            AnonymousPanel.Visible = false;
            LoggedInPanel.Visible = true;
        }
        else
        {
            AnonymousPanel.Visible = true;
            LoggedInPanel.Visible = false;
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        
        //if(Page.IsValid)

        var manager = new UserManager();

        ApplicationUser user = manager.Find(Email.Text, Password.Text);

        if (user != null)
        {
            IdentityHelper.SignIn(manager, user, RememberMe.Checked);
            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }
        else
        {
            sucess.Text = "Invalid e-mail or password.";
        }
    }
}