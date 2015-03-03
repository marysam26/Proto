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

        // This is also the classView, the teacher home defaults to viewing their classes
        public ActionResult Index()
        {
            //var models = new List<ClassViewModel>();
            var courses = DocumentSession.Query<ClassViewModel, ViewClassesIndex>()
                // How to make it pull based on teacherID?
                               .Where(r => r.teacherID == "Teacher1")// How to pull all classes for this teacher?
                               .ToList();

            return View(courses);
            //return View();
        }

        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(AddClassInput input)
        {
            var course = new ClassViewModel()
            {
                classID = input.classID,
                className = input.className,
                teacherID = input.teacherID
            };
            DocumentSession.Store(course);
            DocumentSession.SaveChanges();

            return View();// Want to return to index, but it is NULL on the (!model.any())

        }

        public ActionResult AddStudent()
        {
            return View();
        }

        // Students are enrolling themselves by adding classCodes
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
        public ActionResult ViewStudents(String classID)
        {
            var students = DocumentSession.Query<StudentViewModel, ViewStudentsIndex>()
                // How to make it pull based on teacherID?
                                .Where(r => r.Name == "12345 67890")
                                .Where(r => r.classID == classID)// classID associated with the link from the class button
                                .ToList();

            return View(students);

        }
        
        // I need to make this teacher specific
        public ActionResult ViewClasses(string teacherID)
        {
            var courses = DocumentSession.Query<ClassViewModel, ViewStudentsIndex>()
                // How to make it pull based on teacherID?
                                .Where(r => r.teacherID == teacherID)// classID associated with the link from the class button
                                .ToList();

            return View(courses);

        }

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