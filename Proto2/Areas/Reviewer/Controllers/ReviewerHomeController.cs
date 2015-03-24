using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Proto2.Areas.Account;
using Proto2.Areas.Reviewer.Indexes;
using Proto2.Areas.Reviewer.Models;
using Raven.Client;
using Raven.Client.Document;
using Microsoft.AspNet.Identity;
using Proto2.Areas.Teacher.Models;
using RavenDB.AspNet.Identity;

namespace Proto2.Areas.Reviewer.Controllers
{
    public class ReviewerHomeController : Controller
    {
        //This will get set by dependency injection. Look at DependencyResolution\RavenRegistry
        public IDocumentSession DocumentSession { get; set; }
        //
        // GET: /Reviewer/

           public ReviewerHomeController()
        {
            this.UserManager = new UserManager<ProtoUser>(
                new UserStore<ProtoUser>(() => this.DocumentSession));
               
        }

        //public AccountController(UserManager<ProtoUser> userManager)
        //{
        //    UserManager = userManager;
        //}

        public UserManager<ProtoUser> UserManager { get; private set; }
        public ActionResult Index()
        {
          
            var reviewerModel = DocumentSession.Load<ReviewerModel>(User.Identity.Name);
            //var courses = DocumentSession.Query<ClassModel>().
              //Where(x => reviewerModel.ClassIDs.Contains<string>(x.Id)).ToList();//broken here!!!!!!!!!!!!!!!!!!!!!!!
            
            var courses = new List<ViewModel>();
            foreach (string r in reviewerModel.ClassIDs)
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
            courses.First().Reviewers.Add(User.Identity.GetUserId());

            DocumentSession.SaveChanges();

            return RedirectToAction("Index");

        }

        
        public ActionResult ReviewerAddClass()
        {
            return View();
        }


        public ActionResult Train()
        {
            var trainingVideos = new List<TrainVideoView>()
            {
                //default training videos, to be replaced
                new TrainVideoView(){
                       Title = "Default Training Video",
                       Link = "https://www.youtube.com/watch?v=D85NqSrpzew"
                }
            };
            return View(trainingVideos);
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

        public ActionResult ReviewStoryView()
        {
            //Return a view to review a story
            return View();
        }

        [HttpPost]
        public ActionResult ReviewStoryView(ReviewInput input)
        {
            var newReview = new ReviewInput

            {
                //Information for the new review will be parsed and added to the database here
                //TODO: Parse title,story,and reviewer names from avabile information
                StoryId = input.StoryId,
                ScorePlot = input.ScorePlot,
                Comments = input.Comments,
                ScoreCharacter = input.ScoreCharacter,
                ScoreSetting = input.ScoreSetting,
                Username = User.Identity.GetUserId(),
            };

            DocumentSession.Store(newReview);

            DocumentSession.SaveChanges();

            // Return to the page to choose another review, or return to class
            return Content("It saved!");
            //This will respond to a fom being completed and will eventually be saved to a database
            //This could return a view of all past reviews which would then include the submitted review
            //Or take them to a reviewer conformation page, I will assume the former for now.

        }

        public ActionResult ReviewStory()
        {
            //Return view of a story for review
            var reviewStory = new ReviewViewModel()
            {
                Title = "A Really Great Story",
                Story = "A really great story has really great stories inside.",
                DatePublished = System.DateTime.Now,
                Name = "Samantha B. Rutherferdmanskin",
                NumReviews = 0
            };
            return View(reviewStory);
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
    }
}