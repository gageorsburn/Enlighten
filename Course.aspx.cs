using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enlighten.Models;
using System.IO;
using CodeKicker.BBCode;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.ModelBinding;

public partial class Course : System.Web.UI.Page
{
    public Enlighten.Models.Course course;
    public Enlighten.Models.Member member;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
            Response.Redirect("~/Default.aspx");

        int courseId;
        bool success = int.TryParse(Page.RouteData.Values["Id"].ToString(), out courseId);

        if (!success)
            Response.Redirect("~/Default.aspx");

        ApplicationDbContext dbContext = new ApplicationDbContext();

        course = dbContext.Courses.Where(c => c.Id == courseId).FirstOrDefault();

        member = dbContext.Members.Where(m => m.Email == Context.User.Identity.Name).FirstOrDefault();

        if (!member.Courses.Contains(course) && course.ProfessorId != member.Id)
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
            catch
            {

            }
        }
    }

    protected void NewLessonButton_Click(object sender, EventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        Lesson lesson = new Lesson();

        lesson.Title = NewLessonTitle.Text;
        lesson.Content = NewLessonContent.Text;
        lesson.Course = dbContext.Courses.Where(c => c.Id == course.Id).FirstOrDefault();

        dbContext.Lessons.Add(lesson);
        dbContext.SaveChanges();
        
        LessonRepeater.DataBind();

        HomePanel.Visible = false;
        LessonPanel.Visible = true;

        ActivePanelLabel.Text = "Lessons";

        NewLessonTitle.Text = "";
        NewLessonContent.Text = "";
    }

    protected void DeleteLessonLink_Click(object sender, EventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        Lesson lesson = (Lesson)Session["CurrentLesson"];

        dbContext.Lessons.Remove(dbContext.Lessons.Where(l => l.Id == lesson.Id).FirstOrDefault());
        dbContext.SaveChanges();

        LessonTitleLabel.Text = "";
        LessonContentLabel.Text = "";

        LessonRepeater.DataBind();
        LessonAttachmentRepeater.DataBind();

        HomePanel.Visible = false;
        LessonPanel.Visible = true;

        ActivePanelLabel.Text = "Lessons";
    }

    public IEnumerable<Enlighten.Models.CourseArticle> CourseArticleRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        //Enlighten.Models.Course currentCourse = dbContext.Courses.Where(c => c.Id == course.Id).FirstOrDefault();
        //dbContext.CourseArticles.Include(c => c.Author);
        return dbContext.CourseArticles.Include(c => c.Author).Where(c => c.Course.Id == course.Id).OrderBy(a => a.PostedOn);
    }

    public IEnumerable<Enlighten.Models.CourseUrl> CourseUrlRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        return dbContext.CourseUrls.Where(c => c.Course.Id == course.Id);
    }

    protected void NewArticleButton_Click(object sender, EventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        CourseArticle courseArticle = new CourseArticle();

        courseArticle.Title = NewArticleTitle.Text;
        courseArticle.Content = NewArticleContent.Text;
        courseArticle.PostedOn = DateTime.Now;
        courseArticle.Author = dbContext.Members.Where(m => m.Id == member.Id).FirstOrDefault();
        courseArticle.Course = dbContext.Courses.Where(c => c.Id == course.Id).FirstOrDefault();

        dbContext.CourseArticles.Add(courseArticle);

        dbContext.SaveChanges();

        CourseArticleRepeater.DataBind();

        NewArticleTitle.Text = "";
        NewArticleContent.Text = "";
    }

    protected void DeleteArticleLink_Command(object sender, CommandEventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        int articleId = int.Parse(e.CommandArgument.ToString());

        CourseArticle courseArticle = dbContext.CourseArticles.Where(c => c.Id == articleId).FirstOrDefault();

        dbContext.CourseArticles.Remove(courseArticle);
        dbContext.SaveChanges();

        CourseArticleRepeater.DataBind();
    }

    public IEnumerable<Enlighten.Models.Assignment> AssignmentRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        return dbContext.Courses.Where(c => c.Id == course.Id).FirstOrDefault().Assignments.Reverse();
    }

    protected void AssignmentRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        int assignmentId = int.Parse(e.CommandArgument.ToString());

        Assignment assignment = dbContext.Assignments.Where(a => a.Id == assignmentId).FirstOrDefault();

        AssignmentTitleLabel.Text = assignment.Title;

        try
        {
            AssignmentContentLabel.Text = BBCode.ToHtml(assignment.Content);
        }
        catch
        {
            AssignmentContentLabel.Text = "Some tags did not load correctly<br />" + assignment.Content;
        }

        Session["CurrentAssignment"] = assignment;

        SubmissionRepeater.DataBind();
        GradeSubmissionView.DataBind();

        HomePanel.Visible = false;
        AssignmentPanel.Visible = true;

        ActivePanelLabel.Text = "Assignments";
    }

    protected void DeleteAssignmentLink_Click(object sender, EventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        Assignment assignment = (Assignment)Session["CurrentAssignment"];

        dbContext.Assignments.Remove(dbContext.Assignments.Where(a => a.Id == assignment.Id).FirstOrDefault());
        dbContext.SaveChanges();

        AssignmentTitleLabel.Text = "";
        AssignmentContentLabel.Text = "";

        AssignmentRepeater.DataBind();

        HomePanel.Visible = false;
        AssignmentPanel.Visible = true;

        ActivePanelLabel.Text = "Assignments";
    }

    protected void SubmissionButton_Click(object sender, EventArgs e)
    {
        if (SubmissionUpload.HasFile)
        {
            try
            {
                ApplicationDbContext dbContext = new ApplicationDbContext();

                string fileName = Path.GetFileName(SubmissionUpload.FileName);
                string fileType = Path.GetExtension(fileName).Replace(".", "");

                Assignment CurrentAssignment = (Assignment)Session["CurrentAssignment"];

                Assignment assignment = dbContext.Assignments.Where(a => a.Id == CurrentAssignment.Id).FirstOrDefault();

                var sub = dbContext.Submissions.Where(s => s.Member.Id == member.Id && s.Assignment.Id == assignment.Id && s.Course.Id == course.Id).FirstOrDefault();

                Submission submission;

                if (sub != null)
                    submission = dbContext.Submissions.Find(sub.Id);
                else
                    submission = new Submission();

                submission.Title = SubmissionUpload.FileName;
                submission.FileType = fileType;
                submission.Data = SubmissionUpload.FileBytes;

                submission.Assignment = dbContext.Assignments.Where(a => a.Id == CurrentAssignment.Id).FirstOrDefault();
                submission.Member = dbContext.Members.Where(m => m.Id == member.Id).FirstOrDefault();
                submission.Course = dbContext.Courses.Where(c => c.Id == course.Id).FirstOrDefault();

                if (sub == null)
                    dbContext.Submissions.Add(submission);

                dbContext.SaveChanges();

                SubmissionRepeater.DataBind();

                HomePanel.Visible = false;
                AssignmentPanel.Visible = true;

                ActivePanelLabel.Text = "Assignments";
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Response.Write(string.Format("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage));
                    }
                }
            }
        }
    }

    protected void NewAssignmentButton_Click(object sender, EventArgs e)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        Assignment assignment = new Assignment();

        assignment.Title = NewAssignmentTitle.Text;
        assignment.PossiblePoints = int.Parse(NewAssignmentPossiblePoints.Text);
        assignment.Content = NewAssignmentContent.Text;
        
        assignment.Course = dbContext.Courses.Where(c => c.Id == course.Id).FirstOrDefault();

        dbContext.Assignments.Add(assignment);

        dbContext.SaveChanges();

        AssignmentRepeater.DataBind();

        NewAssignmentTitle.Text = "";
        NewAssignmentPossiblePoints.Text = "";
        NewAssignmentContent.Text = "";

        HomePanel.Visible = false;
        AssignmentPanel.Visible = true;

        ActivePanelLabel.Text = "Assignments";
    }

    public IEnumerable<Enlighten.Models.Submission> SubmissionRepeater_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        Assignment CurrentAssignment = (Assignment)Session["CurrentAssignment"];

        if (CurrentAssignment == null)
            return null;

        Assignment assignment = dbContext.Assignments.Where(a => a.Id == CurrentAssignment.Id).FirstOrDefault();

        return dbContext.Submissions.Where(s => s.Member.Id == member.Id && s.Assignment.Id == assignment.Id && s.Course.Id == course.Id);
    }

    protected void SubmissionRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int submissionId = int.Parse(e.CommandArgument.ToString());

            DownloadSubmission(submissionId);
        }
    }

    public void DownloadSubmission(int submissionId)
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        Submission submission = dbContext.Submissions.Where(la => la.Id == submissionId).FirstOrDefault();

        string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + "." + submission.FileType;
        FileInfo fileInfo = new FileInfo(fileName);

        using (var stream = fileInfo.OpenWrite())
        {
            stream.Write(submission.Data, 0, submission.Data.Count());
        }

        Response.ContentType = submission.FileType;
        Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}.{1}", submission.Title, submission.FileType));
        Response.TransmitFile(fileInfo.FullName);
        Response.End();
    }

    // The return type can be changed to IEnumerable, however to support
    // paging and sorting, the following parameters must be added:
    //     int maximumRows
    //     int startRowIndex
    //     out int totalRowCount
    //     string sortByExpression
    public IQueryable<Enlighten.Models.Submission> GradeSubmissionView_GetData()
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        Assignment CurrentAssignment = (Assignment)Session["CurrentAssignment"];

        if (CurrentAssignment == null)
            return null;

        Assignment assignment = dbContext.Assignments.Where(a => a.Id == CurrentAssignment.Id).FirstOrDefault();

        return dbContext.Submissions.Include(s => s.Member).Where(s => s.Assignment.Id == assignment.Id && s.Course.Id == course.Id);
    }

    protected void DownloadSubmissionLink_Command(object sender, CommandEventArgs e)
    {

    }

    protected void GradeSubmissionView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        HomePanel.Visible = false;
        AssignmentPanel.Visible = true;

        ActivePanelLabel.Text = "Assignments";
    }

    protected void GradeSubmissionView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        HomePanel.Visible = false;
        AssignmentPanel.Visible = true;

        ActivePanelLabel.Text = "Assignments";
    }

    // The id parameter name should match the DataKeyNames value set on the control
    public void GradeSubmissionView_UpdateItem(int id)
    {
        Enlighten.Models.Submission item = null;

        ApplicationDbContext dbContext = new ApplicationDbContext();

        item = dbContext.Submissions.Find(id);

        if (item == null)
        {
            // The item wasn't found
            ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
            return;
        }

        var originalItem = dbContext.Submissions.Include(c => c.Member).Include(c => c.Assignment).Where(s => s.Id == id).FirstOrDefault();

        item.Course = dbContext.Courses.Where(c => c.Id == course.Id).FirstOrDefault();
        item.Member = dbContext.Members.Where(m => m.Id == originalItem.Member.Id).FirstOrDefault();
        item.Assignment = dbContext.Assignments.Where(a => a.Id == originalItem.Assignment.Id).FirstOrDefault();

        TryUpdateModel<Submission>(item);

        if (ModelState.IsValid)
        {
            dbContext.SaveChanges();
        }
    }

    protected void GradeSubmissionView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HomePanel.Visible = false;
        AssignmentPanel.Visible = true;

        ActivePanelLabel.Text = "Assignments";
    }

    protected void DownloadStudentSubmissionLink_Command(object sender, CommandEventArgs e)
    {
        int submissionId = int.Parse(e.CommandArgument.ToString());
        DownloadSubmission(submissionId);
    }
}