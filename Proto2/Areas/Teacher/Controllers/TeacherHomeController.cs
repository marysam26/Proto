using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Proto2.Areas.SystemAdmin.Models;
using Proto2.Areas.Teacher.Models;
using Proto2.Areas.Teacher.Indexes;
using System.Linq;
using Raven.Client;
using StoryView = Proto2.Areas.Teacher.Models.StoryView;


namespace Proto2.Areas.Teacher.Controllers
{
    public class TeacherHomeController : Controller
    {
        //This will get set by dependency injection. Look at DependencyResolution\RavenRegistry
        public IDocumentSession DocumentSession { get; set; }

        // This is also the classView, the teacher home defaults to viewing their classes
        public ActionResult Index()
        {
            var courses = DocumentSession.Query<ClassModel>()
                // How to make it pull based on teacherID
                               .Where(r => r.teacherId == User.Identity.GetUserId())
                               .ToList();

            return View(courses);
        }

        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(AddClassInput input)
        {
            var random = new Random();
            var code = random.Next(1000, 9999);
            var course = new ClassModel()
            {
                id = Guid.NewGuid(),
                ClassName = input.ClassName,
                teacherId = User.Identity.GetUserId(),
                EndDate = input.EndDate,
                ConfirmCode = code.ToString(),
                Students =  new List<string>().ToArray(),
                Reviewers = new List<string>().ToArray(),
            };
            DocumentSession.Store(course);
            DocumentSession.SaveChanges();

            return RedirectToAction("Index");

        }
        // Students are enrolling themselves by adding classCodes
        /* 
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

         }*/

        //TODO:  Pulling data from the database using fake data until the class view for teacher is implemented
        public ActionResult ViewStudents(String classID)
        {
            var students = DocumentSession.Query<StudentViewModel>()
                // How to make it pull based on teacherID?
                                .Where(r => r.teacherID == User.Identity.GetUserId() &&
                                 r.classID == classID)// classID associated with the link from the class button
                                .ToList();

            return View(students);

        }

        public ActionResult ViewReviewer(string clasId)
        {
            var reviewers = DocumentSession.Query<ReviewerViewModel>()
                // How to make it pull based on teacherID?
                            .Where(r => r.classID == User.Identity.GetUserId())
                            .ToList();

            return View(reviewers);
        }
        
        public ActionResult ViewClasses(string teacherId)
        {
            var courses = DocumentSession.Query<ClassModel, ViewStudentsIndex>()
                // How to make it pull based on teacherID?
                                .Where(r => r.teacherId == teacherId)// classID associated with the link from the class button
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

        public ActionResult ViewAssignments(Guid classid)
        {
            var assignments = DocumentSession.Query<AssignmentInputView>()
                .Where(x => x.CourseId == classid).ToList();
            var assignmentList = new AssignmentViewList()
            {
                Assignments = assignments,
                CourseId = classid
            };

            return View(assignmentList);
        }

        public ActionResult ViewAddAssignments(Guid classid)
        {
           var assignments = DocumentSession.Query<AssignmentView>().ToList();
           

            var assignmentAdd = new AssignmentAddInput()
            {
                Assignments = assignments,
                CourseId = classid
            };
            return View(assignmentAdd);
        }

        public ActionResult ViewAssignmentDetails(Guid classid)
        {
            throw new NotImplementedException();
        }

        public ActionResult AddAssignment(AssignmentAddInput course)
        {
            return View();
        }

    

        public ActionResult ViewAssignmentDetailed(AssignmentView asgn)
        {

            return View(asgn);
        }
        public ActionResult ViewAddAssignmentDetailed(Guid asgnId, Guid courseId)
        {
            var asgn = DocumentSession.Load<AssignmentView>(asgnId);
            var addAsgn = new AssignmentInputView()
            {
                AssignmentName = asgn.AssignmentName,
                Description = asgn.Description,
                CourseId = courseId,
                Id = Guid.NewGuid(),
                Link = asgn.Link,
                DueDate = DateTime.Now.AddDays(7)
            };

            return View(addAsgn);
        }
        [HttpPost]
        public ActionResult ViewAddAssignmentDetailed(AssignmentInputView asgn)
        {
            
            DocumentSession.Store(asgn);
            DocumentSession.SaveChanges();

            return RedirectToAction("ViewAssignments", new {classid = asgn.CourseId});
        }
    }

}