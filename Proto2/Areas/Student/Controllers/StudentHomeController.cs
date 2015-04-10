using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;
using WriteItUp2.Areas.Account;
using Raven.Client;
using Microsoft.AspNet.Identity;
using Raven.Client.Document;
using WriteItUp2.Areas.Teacher.Models;
using WriteItUp2.Areas.Reviewer.Models;
using WriteItUp2.Areas.Student.Indexes;
using WriteItUp2.Areas.Student.Models;
using RavenDB.AspNet.Identity;

namespace WriteItUp2.Areas.Student.Controllers
{
    [Authorize(Roles=WriteItUpRoles.Student)]
    public class StudentHomeController : Controller
    {
        //This will get set by dependency injection. Look at DependencyResolution\RavenRegistry
        public IDocumentSession DocumentSession { get; set; }
  

           public StudentHomeController()
           {
            this.UserManager = new UserManager<WriteItUpUser>(
                new UserStore<WriteItUpUser>(() => this.DocumentSession));
        }

        //public AccountController(UserManager<WriteItUpUser> userManager)
        //{
        //    UserManager = userManager;
        //}

        public UserManager<WriteItUpUser> UserManager { get; private set; }
        //
        // GET: /Student/
        public ActionResult Index()
        {
            var userName = "StudentModels/" + User.Identity.Name;
            var models = new List<StudentClassModel>();
            var courses = DocumentSession.Query<ClassModel>()
                         .ToList();


            if (courses.FirstOrDefault() != null)
            {
                for (int i = 0; i < courses.Count; i++)
                {
                    if (courses[i].Students != null)
                    {
                        if (courses[i].Students.Contains(userName))
                        {
                            var teacher = DocumentSession.Load<WriteItUpUser>(courses[i].teacherId);
                            StudentClassModel sClass = new StudentClassModel()
                            {
                                TeacherName = teacher.FirstName + " " + teacher.LastName,
                                EndDate = courses[i].EndDate,
                                ClassName = courses[i].ClassName,
                                courseId = courses[i].Id
                            };
                            models.Add(sClass);
                        }
                    }
                }
            }
            return View(models);
        }

        public ActionResult StudentAddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentAddClass(StudentAddClass input)
        {
            if (input != null)
            {
                //Query classes for this confCode and then add student to the list
                //If there is one, then add load that specific object and add the student to the array
                var courses = DocumentSession.Query<ClassModel, StudentAddClassIndex>()
                            .Where(c => c.ConfirmCode == input.classCode)
                            .ToList();

                if (!courses.Any())  //check to be sure the code correspondes to a class in the DB
                {
                    ModelState.AddModelError("", "The provided class code is incorrect.");
                    return View(input);
                }
                var userName = "StudentModels/" + User.Identity.Name;
                var student = DocumentSession.Load<StudentModel>(userName);

                if (courses.Count != 0 && student != null)
                {

                    var id = courses[0].Id;
                    // Having this Id attribute that gets set by RavenDb 
                    // allows for retrieval of the exact object that can be updated or deleted
                    // by using the Load command that uses a document Id
                    ClassModel course = DocumentSession.Load<ClassModel>(id);
                    List<string> list = course.Students.ToList();
                    list.Add(userName);
                    course.Students = list;
                    //DocumentSession.SaveChanges();

                    string ids = student.Id;
                    // Having this Id attribute that gets set by RavenDb 
                    // allows for retrieval of the exact object that can be updated or deleted
                    // by using the Load command that uses a document Id
                    StudentModel st = DocumentSession.Load<StudentModel>(ids);
                    List<Guid> listS = st.ClassIDs.ToList();
                    listS.Add(course.Id);
                    st.ClassIDs = listS.ToArray();
                
                    DocumentSession.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult ViewAssignments(Guid classID)
        {
            var userName = "StudentModels/" + User.Identity.Name;
            if (classID != null)
            {
            var assigns = new AssignmentsView();

            ClassModel cm = DocumentSession.Load<ClassModel>("ClassModels/" + classID);

            assigns.className = cm.ClassName;

            var assign = DocumentSession.Query<AssignmentInputView>()
                          .Where(a => a.CourseId == classID && a.DueDate > DateTime.Now)
                          .ToList();

            var submissions = DocumentSession.Query<SubmissionView>()
                               .Where(s => s.StudentId == userName && s.classId == classID)
                               .ToList();

            if (assign.Count() != 0)
            {
                assigns.Current = assign.ToArray();
            }

            if (submissions.Count != 0)
            {
                assigns.Submitted = submissions.ToArray();
            }

            return View(assigns);
        }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult CurrentAssignment(Guid Id)
        {
            if (Id != null) { 
            var assignment = DocumentSession.Load<AssignmentInputView>(Id);

            return View(assignment);
        }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult PastSubmission(string submitId, Guid classID)
        {
            if (submitId != null && classID != null)
            {
                SubmissionView submission = DocumentSession.Load<SubmissionView>(submitId);

                SubmitDetails sd = new SubmitDetails()
                {
                    classId = classID,
                    StoryTitle = submission.StoryTitle,
                    Story = new HtmlString(submission.Story),
                    SubmissionId = submission.Id,
                    AssignmentName = submission.AssignmentName,
                    Description = submission.Description,
                    NumReviews = submission.NumReviews
                };
                return View(sd);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Write(Guid Id)
        {
            var userName = "StudentModels/" + User.Identity.Name;
            if (Id != null)
            {
                // Get SubmissionView that matches this assignment id and user
                var prev = DocumentSession.Query<SubmissionView>()
                        .Where(a => a.StudentId == userName && a.AssignmentId == Id)
                        .ToList();

                // If first time loading write page, make a StoryInput Model and return it
                if (prev.Count == 0)
                {
                // Load assignmentInputView with this Id
                var assign = DocumentSession.Load<AssignmentInputView>(Id);
                var writeData = new SubmissionView()
                {
                    
                    classId = assign.CourseId,
                    AssignmentId = assign.Id,
                    DueDate = assign.DueDate,
                    StudentId = userName,
                    AssignmentName = assign.AssignmentName,
                    Description = assign.Description,
                    Story = "",
                    StoryTitle = "",
                    NumReviews = 0
                };
                DocumentSession.Store(writeData);
                DocumentSession.SaveChanges();
                return View(writeData);
            }
            // Else return the SubmissionView from query
            return View(prev[0]);
        }
            else
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Write(SubmissionView input)
        {
            // Load the submissionView with the Id of the one from input
            var sv = DocumentSession.Load<SubmissionView>(input.Id);

            // Update the story data
            sv.Story = input.Story;
            // Add the story title
            sv.StoryTitle = input.StoryTitle;
            // Story and submission date will continue to apdate as long as the 
            // student is making changes before the due date because current assignment will expire at a due date
            sv.SubmissionDate = DateTime.Now;

            DocumentSession.Store(sv);
            DocumentSession.SaveChanges();

            return RedirectToAction("Write", new { Id = sv.AssignmentId });
        }

        public ActionResult Train(Guid Id)
        {
            if (Id != null)
            {
            var assign = DocumentSession.Load<AssignmentInputView>(Id);
            return View(assign);
        }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult BrainStorm(Guid Id)
        {
            if (Id != null)
            {
            var assign = DocumentSession.Load<AssignmentInputView>(Id);
            return View(assign);
        }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult StoryReview(string submissionId)
        {
            if (submissionId != null)
            {
            // StoryId will be passed as a SubmissionId from a submitted assignment
            // that is past it's due date. for example, the reviewer gets all assignments where due date is < DateTime.Now
            // Then looks for submissions with those assignmentIds, then those submissions are listed as ones to review
            
            var StoryReviewsList = new List<StoryReviewView>();

            var reviews = DocumentSession.Query<ReviewInputDatabase>()
                            .Where(r => r.SubmitId == submissionId)
                            .ToList(); // This should only be two, reviews should not show up for reviewer after 2 have been completed

            SubmissionView sv = DocumentSession.Load<SubmissionView>(submissionId);

            int num = 0;
            foreach (ReviewInputDatabase r in reviews)
            {
                StoryReviewsList.Add(new StoryReviewView()
                {
                    classId = sv.classId,
                    submitId = r.SubmitId,
                    AssignmentName = sv.AssignmentName,
                    ScorePlot = r.ScorePlot,
                    ScoreCharacter = r.ScoreCharacter,
                    ScoreSetting = r.ScoreSetting,
                    Comments = r.Comments,
                    reviewNum = num + 1
                });
            }
            return View(StoryReviewsList);
        }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult StoryReviewOne(string submissionId)
        {
            if (submissionId != null)
            {
                // StoryId will be passed as a SubmissionId from a submitted assignment
                // that is past it's due date. for example, the reviewer gets all assignments where due date is < DateTime.Now
                // Then looks for submissions with those assignmentIds, then those submissions are listed as ones to review

                var StoryReviewsList = new List<StoryReviewView>();

                var reviews = DocumentSession.Query<ReviewInputDatabase>()
                                .Where(r => r.SubmitId == submissionId)
                                .ToList(); // This should only be one in this case

                SubmissionView sv = DocumentSession.Load<SubmissionView>(submissionId);

                foreach (ReviewInputDatabase r in reviews)
                {
                    StoryReviewsList.Add(new StoryReviewView()
                    {
                        classId = sv.classId,
                        submitId = r.SubmitId,
                        AssignmentName = sv.AssignmentName,
                        ScorePlot = r.ScorePlot,
                        ScoreCharacter = r.ScoreCharacter,
                        ScoreSetting = r.ScoreSetting,
                        Comments = r.Comments,
                        reviewNum = 1
                    });
                }
                return View(StoryReviewsList);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}