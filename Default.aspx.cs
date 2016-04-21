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

using System.Data.Entity;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /* If user is logged in display courses the member is in and
           if the user is logged out display a login controls.  */
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
        /* Create the database connection. */
        ApplicationDbContext dbContext = new ApplicationDbContext();

        string email = Email.Text;
        string password = Password.Text;

        /* Load member object from email supplied by user. */
        Member authenticatedMember = dbContext.Members.Where(m => m.Email == email).FirstOrDefault();

        /* Check to see if that email exists in the member table. */
        if (authenticatedMember != null)
        {
            /* Verify password supplied by user hashed matches the password hash for that member object. */
            PasswordVerificationResult verifiedResult = new PasswordHasher().VerifyHashedPassword(authenticatedMember.Password, password);

            if (verifiedResult.HasFlag(PasswordVerificationResult.Success))
            {
                /* User can be authenticated. */

                /* The code below uses Microsoft's identity and authentication service to authenticate users.
                   It is stripped down to the bare minimum to work with our member model without having all
                   of the extra Microsoft tables that we do not need for this application. It is kind of 
                   confusing on how it work so I won't explain specifically how it works. */
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, authenticatedMember.Email),
                new Claim(ClaimTypes.Name, authenticatedMember.Email)
            };

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = RememberMe.Checked }, identity);

                /* After user is signed in, redirect to home page to display courses. */
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                /* User cannot be authenticated.*/

                SuccessLabel.ForeColor = System.Drawing.Color.Red;
                SuccessLabel.Text = "Invalid e-mail or password.";
            }
        }
        else
        {
            /* User cannot be authenticated.*/

            SuccessLabel.ForeColor = System.Drawing.Color.Red;
            SuccessLabel.Text = "Invalid e-mail or password.";
        }
    }

    public System.Collections.IEnumerable CourseRepeater_GetData()
    {
        /* Create the database connection. */
        ApplicationDbContext dbContext = new ApplicationDbContext();

        /* Get the logged in users email from the authentication service. */
        var email = Context.User.Identity.GetUserName();

        /* Get the member object from the email. */
        Member authenticatedMember = dbContext.Members.Where(m => m.Email == email).FirstOrDefault();

        /* If the member object is null nothing will be displayed. Should never be called. */
        if (authenticatedMember == null)
            return null;

        /* Return all the courses the member is enrolled in. */
        return dbContext.Courses.Include(c => c.Location).Where(m => m.Students.Any(x => x.Id == authenticatedMember.Id) || m.ProfessorId == authenticatedMember.Id || m.AssistantId == authenticatedMember.Id);

    }

    public Member GetProfessorById(int Id)
    {
        /* Create the database connection. */
        ApplicationDbContext dbContext = new ApplicationDbContext();

        return dbContext.Members.Where(m => m.Id == Id).FirstOrDefault();
    }
}