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
                    Location = "G100",
                    Time = "TR 2:00PM-3:00PM"
                };

                applicationDbContext.Members.Add(member);
                applicationDbContext.Courses.Add(course);

                applicationDbContext.SaveChanges();
            }    
    }

</script>
