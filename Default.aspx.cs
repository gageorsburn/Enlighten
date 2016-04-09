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

            CourseRepeater.DataSource = AuthenticatedMemberCourses;
            CourseRepeater.DataBind();
        }
        else
        {
            AnonymousPanel.Visible = true;
            LoggedInPanel.Visible = false;
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        CourseDbContext dbContext = new CourseDbContext();

        string email = Email.Text;
        string password = Password.Text;

        Member authenticatedMember = dbContext.Members.Where(m => m.Email == email).FirstOrDefault();

        PasswordVerificationResult verifiedResult = new PasswordHasher().VerifyHashedPassword(authenticatedMember.Password, password);
        if(verifiedResult.HasFlag(PasswordVerificationResult.Success))
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
        }
        else
        {
            sucess.Text = "Invalid e-mail or password.";
        }
    }

    public ICollection<Course> AuthenticatedMemberCourses
    {
        get
        {
            CourseDbContext dbContext = new CourseDbContext();

            var email = Context.User.Identity.GetUserName();
            Member authenticatedMember = dbContext.Members.Where(m => m.Email == email).FirstOrDefault();
            return authenticatedMember.Courses;
        }
    }
}