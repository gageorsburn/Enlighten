using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enlighten.Models;

public partial class Course : System.Web.UI.Page
{
    public Enlighten.Models.Course course;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
            Response.Redirect("~/Default.aspx");

        int courseId;
        bool success = int.TryParse(Request.QueryString["Id"], out courseId);

        if(!success)
            Response.Redirect("~/Default.aspx");

        ApplicationDbContext dbContext = new ApplicationDbContext();
        course = dbContext.Courses.Where(c => c.Id == courseId).FirstOrDefault();

        CourseTitleLabel.Text = course.Title;
        ActivePanelLabel.Text = "Course Home";

        HomePanel.Visible = true;
        LessonPanel.Visible = false;
        AssignmentPanel.Visible = false;
        GradePanel.Visible = false;
        ClassListPanel.Visible = false;
    }



    protected void HomeHyperLink_Click(object sender, EventArgs e)
    {
        ActivePanelLabel.Text = "Course Home";

        HomePanel.Visible = true;
        LessonPanel.Visible = false;
        AssignmentPanel.Visible = false;
        GradePanel.Visible = false;
        ClassListPanel.Visible = false;
    }

    protected void LessonHyperLink_Click(object sender, EventArgs e)
    {
        ActivePanelLabel.Text = "Lessons";

        HomePanel.Visible = false;
        LessonPanel.Visible = true;
        AssignmentPanel.Visible = false;
        GradePanel.Visible = false;
        ClassListPanel.Visible = false;
    }

    protected void AssignmentHyperLink_Click(object sender, EventArgs e)
    {
        ActivePanelLabel.Text = "Assignments";

        HomePanel.Visible = false;
        LessonPanel.Visible = false;
        AssignmentPanel.Visible = true;
        GradePanel.Visible = false;
        ClassListPanel.Visible = false;
    }

    protected void GradeHyperLink_Click(object sender, EventArgs e)
    {
        ActivePanelLabel.Text = "Grades";

        HomePanel.Visible = false;
        LessonPanel.Visible = false;
        AssignmentPanel.Visible = false;
        GradePanel.Visible = true;
        ClassListPanel.Visible = false;
    }

    protected void ClassListHyperLink_Click(object sender, EventArgs e)
    {
        ActivePanelLabel.Text = "Class List";

        HomePanel.Visible = false;
        LessonPanel.Visible = false;
        AssignmentPanel.Visible = false;
        GradePanel.Visible = false;
        ClassListPanel.Visible = true;
    }
}