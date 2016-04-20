<%@ Application Language="C#" %>
<%@ Import Namespace="Enlighten" %>
<%@ Import Namespace="Enlighten.Models" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Data.Entity" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        
        Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        if(applicationDbContext.Members.Count() == 0)
        {
            Member member = new Member()
            {
                FirstName = "Gage",
                LastName = "Orsburn",

                Email = "gage.orsburn@okstate.edu",
                Password = "AEjQD+DHLN0QCeh2HWtYnIQAoRZWSZly962xi6cMVCLSRU1eyxCueGfSA4QiXyoOqw==",

                Picture = System.IO.File.ReadAllBytes(Server.MapPath("Content/4692734.jpg"))
            };

            Course course = new Course()
            {
                Title = "Programming",
                Section = "001",
                ProfessorId = 1,
                AssistantId = 1,
                Description = "Computer programming for organizations from the perspective of integrating the Internet into business information systems. Fundamental principles and constructs of programming and applied programming in the business environment.",
                
                Time = "TR 2:00PM-3:00PM"
            };

            Lesson lesson = new Lesson()
            {
                Title = "Lesson 1",
                Content = "Content for lesson 1.",
                Course = course
            };

            LessonAttachment lessonAttachment = new LessonAttachment()
            {
                Title = "File",
                FileType = "txt",
                Data = System.IO.File.ReadAllBytes(Server.MapPath("App_Data/File.txt")),
                Lesson = lesson
            };

            applicationDbContext.Members.Add(member);
            applicationDbContext.Courses.Add(course);

            applicationDbContext.Lessons.Add(lesson);
            applicationDbContext.LessonAttachments.Add(lessonAttachment);

            applicationDbContext.SaveChanges();
        }
    }

</script>
