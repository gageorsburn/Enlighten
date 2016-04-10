using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Enlighten;
using Enlighten.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;

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
        ApplicationDbContext dbContext = new ApplicationDbContext();

        string email = Email.Text;
        string password = Password.Text;

        Member authenticatedMember = dbContext.Members.Where(m => m.Email == email).FirstOrDefault();

        if (authenticatedMember != null)
        {
            PasswordVerificationResult verifiedResult = new PasswordHasher().VerifyHashedPassword(authenticatedMember.Password, password);
            if (verifiedResult.HasFlag(PasswordVerificationResult.Success))
            {
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, authenticatedMember.Email),
                new Claim(ClaimTypes.Name, authenticatedMember.Email)
            };

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = RememberMe.Checked }, identity);

                Response.Redirect("~/Default.aspx");
            }
            else
            {
                SuccessLabel.ForeColor = System.Drawing.Color.Red;
                SuccessLabel.Text = "Invalid e-mail or password.";
            }
        }
        else
        {
            SuccessLabel.ForeColor = System.Drawing.Color.Red;
            SuccessLabel.Text = "Invalid e-mail or password.";
        }
    }

    public System.Collections.IEnumerable CourseRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        var email = Context.User.Identity.GetUserName();
        Member authenticatedMember = dbContext.Members.Where(m => m.Email == email).FirstOrDefault();
        return authenticatedMember.Courses;
    }

    public Member GetProfessorById(int Id)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        return dbContext.Members.Where(m => m.Id == Id).FirstOrDefault();
    }
}