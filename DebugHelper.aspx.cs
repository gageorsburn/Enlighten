using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enlighten.Models;

public partial class DebugHelper : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Request.IsAuthenticated)
            Response.Redirect("~/Default.aspx");
    }

    public IEnumerable<Enlighten.Models.Course> CourseRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        return dbContext.Courses;
    }

    public IEnumerable<Enlighten.Models.Member> MemberRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        return dbContext.Members;
    }
}