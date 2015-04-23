using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using WriteItUp2.Areas.Account;
using WriteItUp2.Areas.Reviewer.Indexes;
using WriteItUp2.Areas.Reviewer.Models;
using WriteItUp2.Areas.SystemAdmin.Models;
using Raven.Client;
using Raven.Client.Document;
using Microsoft.AspNet.Identity;
using WriteItUp2.Areas.Teacher.Models;
using RavenDB.AspNet.Identity;
using WriteItUp2.Areas.Student.Models;
using ClassModel = WriteItUp2.Areas.Teacher.Models.ClassModel;
//using SubmissionView = WriteItUp2.Areas.SystemAdmin.Models.SubmissionView;
using SubmissionView = WriteItUp2.Areas.Student.Models.SubmissionView;

namespace WriteItUp2.Areas.Reviewer.Controllers
{
    [Authorize(Roles=WriteItUpRoles.Reviewer)]
    public class ReviewerHomeController : Controller
    {
        //This will get set by dependency injection. Look at DependencyResolution\RavenRegistry
        public IDocumentSession DocumentSession { get; set; }
        //
        // GET: /Reviewer/

           public ReviewerHomeController()
        {
            this.UserManager = new UserManager<WriteItUpUser>(
                new UserStore<WriteItUpUser>(() => this.DocumentSession));
               
        }

        //public AccountController(UserManager<WriteItUpUser> userManager)
        //{
        //    UserManager = userManager;
        //}

        public UserManager<WriteItUpUser> UserManager { get; private set; }

        public ActionResult Index()
        {
          
            var reviewerModel = DocumentSession.Load<ReviewerModel>(User.Identity.Name);
            //var courses = DocumentSession.Query<ClassModel>().
              //Where(x => reviewerModel.ClassIDs.Contains<string>(x.Id)).ToList();//broken here!!!!!!!!!!!!!!!!!!!!!!!
            
            var courses = new List<ViewModel>();
            foreach (var r in reviewerModel.ClassIDs)
            {
                var course = DocumentSession.Load<ClassModel>(r);  //look through database for every class this reviewer is associated with
                var ReviewerClass = new ViewModel();
                ReviewerClass.ClassIDs = course.Id;
                ReviewerClass.ClassName = course.ClassName;
                ReviewerClass.NumReviews = course.Reviewers.Count;
                courses.Add(ReviewerClass);
            }
            


            return View(courses);
        }


        [HttpPost]
        public ActionResult ReviewerAddClass(ReviewerAddClass input)
        {
            var courses = DocumentSession.Query<ClassModel>()
                   .Where(r => r.ConfirmCode == input.classCode)
                   .ToList();
            if (!courses.Any())  //check to be sure the code correspondes to a class in the DB
            {
                ModelState.AddModelError("", "The provided class code is incorrect.");
                return View(input);
            }

            var reviewer = DocumentSession.Load<ReviewerModel>(User.Identity.Name);  //get this reviewers model from the db

            //add the class to the reviewers model on db
            reviewer.ClassIDs.Add(courses.First().Id);
            
            //add reviewer id to the class model on the database
            courses.First().Reviewers.Add(User.Identity.Name);

            DocumentSession.SaveChanges();

            return RedirectToAction("Index");

        }

        
        public ActionResult ReviewerAddClass()
        {
            return View();
        }


        public ActionResult Train()
        {
                
                //The SystemAdmin view is not currently set up to distinguish between the videos.  
                //It is currently just holding all videos to test my training page.
            //SystemAdmin needs to be updated to distinguish between other videos
                var training = DocumentSession.Query<VideoView>().ToList();
                if (training.Count() != 0)
                {
                    return View(training[0]);
                }
                else
                {
                    return View("NoTrainingVideos");
                }
        }
          
        public ActionResult PastReviewsHome()
        {
            //Default review, will pull reviews from database but will use this as default for now.
            var pastReviewsHome = new List<PastReviewHome>(){
                new PastReviewHome(){
                       Title = "Finished Story ~ Reviewed",
                       DateReviewer = System.DateTime.Now
                }
            };
            return View(pastReviewsHome);
        }

        public ActionResult PastReviews()
        {
            //var userName = "kblooie";


            var pastReviews = DocumentSession.Query<PastReviewView, PastReviewIndex>()
                .Where(r => r.StudentId == User.Identity.GetUserId() && r.PublishDate >= DateTime.UtcNow.AddDays(-7))
                .ToList();

            return View(pastReviews);
        }

        public ActionResult ReviewStoryView(string SubmitId)
        {
            //Return a view to review a story
            var Submission = DocumentSession.Load<SubmissionView>(SubmitId);

           if (Submission.reviewer1 == null)
            {
                Submission.reviewer1 = User.Identity.GetUserId();
                Submission.NumReviews = Submission.NumReviews + 1;
            }
            else
            {
                Submission.reviewer2 = User.Identity.GetUserId();
                Submission.NumReviews = Submission.NumReviews + 1;
            }

            DocumentSession.Store(Submission);

            DocumentSession.SaveChanges();
            ReviewInput input = new ReviewInput()
            {
                AssignmentName = Submission.AssignmentName,
                AssignmentDescription = Submission.Description,
                SubmitId = Submission.Id,
                StoryTitle = Submission.StoryTitle,
                Story = new HtmlString(Submission.Story),
                KeyList = new List<SelectListItem>()
                {
                            new SelectListItem() {Text = "0", Value = "0"},
                            new SelectListItem() {Text = "1", Value = "1"},
                            new SelectListItem() {Text = "2", Value = "2"},
                            new SelectListItem() {Text = "3", Value = "3"},
                            new SelectListItem() {Text = "4", Value = "4"},
                            new SelectListItem() {Text = "5", Value = "5"},
                            new SelectListItem() {Text = "6", Value = "6"},
                            new SelectListItem() {Text = "7", Value = "7"},
                 }
            };
            return View(input);
        }

        [HttpPost]
        public ActionResult ReviewStoryView(ReviewInput input)
        {
            var newReview = new ReviewInputDatabase

            {
                //Information for the new review will be parsed and added to the database here
                //TODO: Parse title,story,and reviewer names from avabile information
                //AssignmentName = input.AssignmentName,
                //AssignmentDescription = input.AssignmentDescription,
                SubmitId = input.SubmitId,
                ScorePlot = input.ScorePlot,
                Comments = input.Comments,
                ScoreCharacter = input.ScoreCharacter,
                ScoreSetting = input.ScoreSetting,
                Username = User.Identity.GetUserId(),               
            };

            DocumentSession.Store(newReview);

            DocumentSession.SaveChanges();

            // Return to the page to choose another review, or return to class
            return RedirectToAction("Index");
            //This will respond to a fom being completed and will eventually be saved to a database
            //This could return a view of all past reviews which would then include the submitted review
            //Or take them to a reviewer conformation page, I will assume the former for now.

        }

        public ActionResult ReviewStories(Guid classId)
        {
            ClassModel course = DocumentSession.Load<ClassModel>(classId);
            //Return view of a story for review

            var storyList = new List<Reviews>();
            var submittedStories = DocumentSession.Query<SubmissionView>().Where(s => s.classId == course.Id && s.DueDate < DateTime.Now && s.NumReviews < 2 ).ToList();

            var userName = User.Identity.GetUserId();

            foreach (SubmissionView s in submittedStories)
            {
                if (s.reviewer1 != userName && s.reviewer2 != userName)
                {
                    storyList.Add(new Reviews()
                    {
                        SubmissionId = s.Id,
                        AssignmentName = s.AssignmentName,
                        NumReviews = s.NumReviews,
                        DatePublished = s.SubmissionDate,
                        StoryTitle = s.StoryTitle
                    }); 
                }
                else
                {
                    return View("NoReviews");
                }
                      
            }
            return View(storyList);
        }

        public ActionResult NoReviews()
        {
            return View();
        }

       [HttpPost]
        public ActionResult ReviewStory(ReviewInput input)
        {
            PastReviewView pastReviews;

            pastReviews = new PastReviewView
            {
                //Information for the new review will be parsed and added to the database here
                //TODO: Parse title,story,and reviewer names from avabile information
                Title = "Reviewed Story",
                Story = "A long long long long but not so long time ago....",
                //ReviewerName = "Uidentfied reviewer",
                //ReviewerNames = new string[] { "Dr. Pompus" },
                Comment = input.Comments,
                ScoreCharacter = input.ScoreCharacter,
                ScorePlot = input.ScorePlot,
                ScoreSetting = input.ScoreSetting,
                //  OwnerUserId = input.OwnerUserId,
                PublishDate = DateTime.UtcNow
            };

            DocumentSession.Store(pastReviews);

            DocumentSession.SaveChanges();

            return Content("It saved!");
            //This will respond to a fom being completed and will eventually be saved to a database
            //This could return a view of all past reviews which would then include the submitted review
            //Or take them to a reviewer conformation page, I will assume the former for now.

        }

        public ActionResult ViewPastReviews()
        {
            //TODO: return view list of PastReviewViews
            //(Casey): Is there a difference between this and PastReviews() above?
            return View();
        }

        public ActionResult Discuss()
        {
            //return Discussion view
            var discussionView = new DiscussionView()
            {
                Title = "",
                Dicussion = new string[][]{
                    new string[]{
                        "Revwier 1",
                        "Comment 1"
                    },
                    new string[]{
                        "Reviwer 2",
                        "Comment 2"
                    }
                },
                ScoreCharacter = 10,
                ScorePlot = 10,
                ScoreSetting = 10
            };
            return View(discussionView);
        }

        [HttpPost]
        public ActionResult Discuss(CommentInput input)
        {
            //TODO: eventually this will respond to a post of a comment and the comment will be saved to database
            //similar to above, assuming this takes reviewer to list of full discussion
            return RedirectToAction("Discuss");
        }

        public ActionResult RegisterasTeacher()
        {
            if (UserManager.IsInRole(User.Identity.GetUserId(), WriteItUpRoles.Teacher))
            {
                return RedirectToAction("Index", "TeacherHome", new { area = "Teacher" });

            }

            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult RegisterAsTeacher(RegisterTeacher input)
        {
            var code = DocumentSession.Load<AddPassView>(input.TeacherCode);
            if (code == null)
            {
                ModelState.AddModelError("", "The provided teacher code is incorrect.");
                return View(input);
            }

            var teacher = new TeacherModel()
            {
                Name = User.Identity.Name,
                Id = "Teacher/" + User.Identity.Name,
                Classes = new List<Guid>()
            };
            DocumentSession.Store(teacher);
            UserManager.AddToRole(User.Identity.GetUserId(), WriteItUpRoles.Teacher);
            DocumentSession.Delete<AddPassView>(input.TeacherCode);
            DocumentSession.SaveChanges();
            while (!User.IsInRole(WriteItUpRoles.Teacher))
            {
            }

            return RedirectToAction("Index", "TeacherHome", new { area = "Teacher" });
        }
    }
}