using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Proto2.Areas.Account;
using Proto2.Areas.Reviewer.Models;
using Proto2.Areas.Student.Models;
using Proto2.Areas.SystemAdmin.Models;
using Proto2.Areas.Teacher.Models;
using Proto2.Areas.Teacher.Indexes;
using System.Linq;
using Raven.Abstractions.Linq;
using Raven.Client;
using RavenDB.AspNet.Identity;
using ClassModel = Proto2.Areas.Teacher.Models.ClassModel;
using StoryView = Proto2.Areas.Teacher.Models.StoryView;


namespace Proto2.Areas.Teacher.Controllers
{

    public class TeacherHomeController : Controller
    {
        //This will get set by dependency injection. Look at DependencyResolution\RavenRegistry
        public IDocumentSession DocumentSession { get; set; }
        public TeacherHomeController()
        {
            this.UserManager = new UserManager<ProtoUser>(
                new UserStore<ProtoUser>(() => this.DocumentSession));
        }

        //public AccountController(UserManager<ProtoUser> userManager)
        //{
        //    UserManager = userManager;
        //}

        public UserManager<ProtoUser> UserManager { get; private set; }
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
                Id = Guid.NewGuid(),
                ClassName = input.ClassName,
                teacherId = User.Identity.GetUserId(),
                EndDate = input.EndDate,
                ConfirmCode = code.ToString(),
                Students =  new List<string>(),
                Reviewers = new List<string>(),
            };

            var teacher = DocumentSession.Load<TeacherModel>("Teacher/" +User.Identity.Name);
            teacher.Classes.Add(course.Id);
           
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
        public ActionResult ViewStudents(Guid classID)
        {
            var studentList = DocumentSession.Query<StudentModel>()
                // How to make it pull based on teacherID?
                                .Where(x => x.ClassIDs.Contains<Guid>(classID))// classID associated with the link from the class button
                                .ToList();
            var students = new StudentViewList()
            {
                StudentList = studentList,
                CourseId = classID
            };
            return View(students);

        }

        public ActionResult ViewReviewer(Guid classID)
        {
            var reviewerList = DocumentSession.Query<ReviewerModel>()
                // How to make it pull based on teacherID?
                              .Where(x => x.ClassIDs.Contains<Guid>(classID))// classID associated with the link from the class button
                              .ToList();
            var reviewers = new ReviewerViewList()
            {
                ReviewerList = reviewerList,
                CourseId = classID
            };
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


        public ActionResult ViewReviews(Guid courseId)
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

       
        public ActionResult DeleteStudent( Guid courseId, string dataId)
        {
            var courses = DocumentSession.Load<ClassModel>(courseId);
            if (courses != null)
            {
                courses.Students.Remove(dataId);// = courses.Students.Where(val => val != student);  //change from array to list of students, may need to be revised
                var random = new Random();
                var code = random.Next(1000, 9999);
                courses.ConfirmCode = code.ToString();
            }

            var studentName = DocumentSession.Load<StudentModel>(dataId);
            if (studentName != null)
            {
                studentName.ClassIDs = studentName.ClassIDs.Where(val => val != courseId).ToArray();
            }
       
            DocumentSession.SaveChanges();

            return RedirectToAction("ViewStudents", new {classid = courseId});
        }

        public ActionResult DeleteReviewer(Guid courseId, string dataId)
        {
            var courses = DocumentSession.Load<ClassModel>(courseId);
            if (courses != null)
            {
                courses.Reviewers.Remove(dataId);// = courses.Students.Where(val => val != student);  //change from array to list of students, may need to be revised
                var random = new Random();
                var code = random.Next(1000, 9999);
                courses.ConfirmCode = code.ToString();
            }

            var reviewer = DocumentSession.Load<ReviewerModel>(dataId);
            if (reviewer != null)
            {
                reviewer.ClassIDs.Remove(courseId);
            }

            DocumentSession.SaveChanges();

            return RedirectToAction("ViewReviewer", new { classid = courseId });
        }

        public ActionResult RegisterAsReviewer()
        {
            if (UserManager.IsInRole(User.Identity.GetUserId(), ProtoRoles.Reviewer))
            {
                return RedirectToAction("Index", "ReviewerHome", new {area = "Reviewer"});

            }
         
            else
            {
                return View();
            }
        }

        public ActionResult RegisterTeacherAsReviewer()
        {
            UserManager.AddToRole(User.Identity.GetUserId(), ProtoRoles.Reviewer);

            var r = new ReviewerModel()
            {
                Id = User.Identity.Name,
                Name = User.Identity.Name,
                ClassIDs = new List<Guid>(),
                Reviews = new List<PastReviewView>()
            };
            DocumentSession.Store(r);
            DocumentSession.SaveChanges();

            return RedirectToAction("Index", "ReviewerHome", new { area = "Reviewer" });
        }

        public ActionResult ExtendDueDate(DateTime date, Guid id)
        {
            ViewData.Add("CurrentDueDate", date);
            
            return View(new AssignmentInputView{Id = id, DueDate = date.AddDays(1)});
        }

        [HttpPost]
        public ActionResult ExtendDueDate(AssignmentInputView assignmentInputView)
        {
            var assign = DocumentSession.Load<AssignmentInputView>(assignmentInputView.Id);
                assign.DueDate = assignmentInputView.DueDate;

                DocumentSession.SaveChanges();
                return RedirectToAction("ViewAssignments", new {classId = assign.CourseId});
        }

        public ActionResult DeleteCourse(Guid courseId)
        {
            var classModel = DocumentSession.Load<ClassModel>(courseId);
            foreach (var s in classModel.Students)
            {
                var studentName = DocumentSession.Load<StudentModel>(s);
                if (studentName != null)
                {
                    studentName.ClassIDs = studentName.ClassIDs.Where(val => val != courseId).ToArray();
                }

            }

            foreach (var r in classModel.Reviewers)
            {
                var reviewer = DocumentSession.Load<ReviewerModel>(r);
                if (reviewer != null)
                {
                    reviewer.ClassIDs.Remove(courseId);
                }
            }
            DocumentSession.Delete<ClassModel>(courseId);
            DocumentSession.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewStudentAssignments(string studentid)
        {
            var userName = "StudentModels/" + User.Identity.Name;
      
                // Get SubmissionView that matches this assignment id and user
                var prev = DocumentSession.Query<SubmissionView>()
                        .Where(a => a.StudentId == studentid)
                        .ToList();
            return View(prev);
                                // If first time loading write page, make a StoryInput Model and return it
               }

        public ActionResult CurrentAssignment(string story)
        {
            var assign = DocumentSession.Load<SubmissionView>(story);
            assign.Story = Regex.Replace(assign.Story, @"<[^>]+>|&nbsp;", "").Trim();
            return View(assign);
        }

        public ActionResult CurrentReview(string revId, string assId)
        {
            var review = DocumentSession.Query<ReviewInputDatabase>().
                Where(x => x.SubmitId == assId && x.Username == revId).ToList().FirstOrDefault();

            return View(review);

        }
    }

}