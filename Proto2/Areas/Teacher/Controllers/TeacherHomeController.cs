using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Proto2.Areas.Teacher.Models;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using Proto2.Areas.Teacher.Indexes;


namespace Proto2.Areas.Teacher.Controllers
{
    public class TeacherHomeController : Controller
    {
        //This will get set by dependency injection. Look at DependencyResolution\RavenRegistry
        public IDocumentSession DocumentSession { get; set; }

        public ActionResult Index()
        {
            var models = new List<StudentViewModel>();
            return View(models);
            //return View();
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(AddStudentInput input)
        {
            var student = new StudentViewModel()
            {
                Confirmed = "Not Confirmed",
                Id = new Guid("ED88C65A-77D4-4C57-976D-7E3E5303E6CA"),
                Name = input.FirstName + " " + input.LastName,
                NumReviews = 0
            };
            DocumentSession.Store(student);
            DocumentSession.SaveChanges();

            return View(student);

        }
        //TODO:  Pulling data from the database using fake data until the class view for teacher is implemented
        public ActionResult ViewStudents()
        {
            var students = DocumentSession.Query<StudentViewModel, ViewStudentsIndex>()
                                .Where(r => r.Name == "12345 67890")
                                .ToList();

            return View(students);

        }
      /*  public ActionResult StudentView()
        {
            var Name = "Tina Roper";

            var studentViews = DocumentSession.Query<StudentViewModel>().Where(r => r.Name == Name);
            return View(studentViews);

        }*/
        public ActionResult ViewStory(Guid studentId)
        {
            var model = new List<StoryView>
            {
                new StoryView()
                {
                    Title = "Best Story Ever Written",
                    Stories ="This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title",
                    Id = Guid.NewGuid()
                },
                new StoryView()
                {
                    Title ="Worst Story Ever",
                    Stories="Boo",
                    Id = Guid.NewGuid()
                }
            };
            return View(model);
        }


        public ActionResult ViewReviews(Guid studentid)
        {
            var model = new List<ReviewView>
            {
                new ReviewView()
                {
                    ReviewerName = "Best Reviewer Ever",
                    Comment = "This was the greatest thing I've ever read",
                    ScoreCharacter = 6,
                    ScorePlot = 7,
                    ScoreSetting = 0,
                    Id = Guid.NewGuid()
                }
            };
            return View(model);
        }
    }

}