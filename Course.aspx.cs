using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enlighten.Models;
using System.IO;
using CodeKicker.BBCode;


public partial class Course : System.Web.UI.Page
{
    public Enlighten.Models.Course course;
    public Enlighten.Models.Member member;

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

        member = dbContext.Members.Where(m => m.Email == Context.User.Identity.Name).FirstOrDefault();

        if (!member.Courses.Contains(course))
            Response.Redirect("~/Default.aspx");

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

    public Member GetProfessor()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        return dbContext.Members.Where(m => m.Id == course.ProfessorId).FirstOrDefault();
    }

    public Member GetAssistant()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        return dbContext.Members.Where(m => m.Id == course.AssistantId).FirstOrDefault();
    }

    public IEnumerable<Enlighten.Models.Member> ClassListRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        return dbContext.Members.Where(m => m.Courses.Any(c => c.Id == course.Id));
    }

    public string GetPictureUrl(Member member)
    {
        if (member.Picture == null)
            return "";

        return "data:image/jpg;base64," + Convert.ToBase64String(member.Picture, 0, member.Picture.Length);
    }

    public IEnumerable<Enlighten.Models.Lesson> LessonRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        return dbContext.Courses.Where(c => c.Id == course.Id).FirstOrDefault().Lessons.Reverse();
    }

    protected void LessonRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        int lessonId = int.Parse(e.CommandArgument.ToString());

        Lesson lesson = dbContext.Lessons.Where(l => l.Id == lessonId).FirstOrDefault();

        LessonTitleLabel.Text = lesson.Title;

        try
        {
            LessonContentLabel.Text = BBCode.ToHtml(lesson.Content);
        }
        catch
        {
            LessonContentLabel.Text = "Some tags did not load correctly<br />" + lesson.Content;
        }

        Session["CurrentLesson"] = lesson;
        //currentLesson = lesson;

        LessonAttachmentRepeater.DataBind();

        HomePanel.Visible = false;
        LessonPanel.Visible = true;

        ActivePanelLabel.Text = "Lessons";
    }

    public IEnumerable<Enlighten.Models.LessonAttachment> LessonAttachmentRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        //if (currentLesson == null)
        if(Session["CurrentLesson"] == null)
            return null;

        //int currentLessonId = currentLesson.Id;
        int currentLessonId = ((Lesson)Session["CurrentLesson"]).Id;

        return dbContext.LessonAttachments.Where(l => l.Lesson.Id == currentLessonId);
    }

    protected void LessonAttachmentRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        if (e.CommandName == "Download")
        {
            int attachmentId = int.Parse(e.CommandArgument.ToString());

            LessonAttachment lessonAttachment = dbContext.LessonAttachments.Where(la => la.Id == attachmentId).FirstOrDefault();

            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + "." + lessonAttachment.FileType;
            FileInfo fileInfo = new FileInfo(fileName);

            using (var stream = fileInfo.OpenWrite())
            {
                stream.Write(lessonAttachment.Data, 0, lessonAttachment.Data.Count());
            }

            Response.ContentType = lessonAttachment.FileType;
            Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}.{1}", lessonAttachment.Title, lessonAttachment.FileType));
            Response.TransmitFile(fileInfo.FullName);
            Response.End();
        }
        else if (e.CommandName == "Upload")
        {

        }
    }

    public bool IsMemberProfessor()
    {
        return course.ProfessorId == member.Id;
    }



    protected void LessonAttachmentButton_Click(object sender, EventArgs e)
    {
        if(LessonAttachmentUpload.HasFile)
        {
            try
            {
                ApplicationDbContext dbContext = new ApplicationDbContext();

                string fileName = Path.GetFileName(LessonAttachmentUpload.FileName);
                string fileType = Path.GetExtension(fileName).Replace(".", "");

                LessonAttachment lessonAttachment = new LessonAttachment();

                lessonAttachment.Title = LessonAttachmentUpload.FileName;
                lessonAttachment.FileType = fileType;
                lessonAttachment.Data = LessonAttachmentUpload.FileBytes;

                Lesson CurrentLesson = (Lesson)Session["CurrentLesson"];

                lessonAttachment.Lesson = dbContext.Lessons.Where(l => l.Id == CurrentLesson.Id).FirstOrDefault();

                dbContext.LessonAttachments.Add(lessonAttachment);

                dbContext.SaveChanges();

                LessonAttachmentRepeater.DataBind();

                HomePanel.Visible = false;
                LessonPanel.Visible = true;

                ActivePanelLabel.Text = "Lessons";
            }
            catch(Exception exception)
            {

            }
        }
    }
}