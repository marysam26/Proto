using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Proto.Areas.Reviewer.Models;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Proto.Areas.Reviewer.Controllers
{
    public class ReviewerHomeController : Controller
    {
        private IDocumentSession session;
        //
        // GET: /Reviewer/

        public ReviewerHomeController()
        {
            session = RavenSingleton.GetSession();
        }

        public ActionResult Index()
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
            var userName = "kblooie";

            
            var pastReviews = session.Query<PastReviewView, PastReviewIndex>()
                .Where(r => r.OwnerUserId == userName && r.PublishDate >= DateTime.UtcNow.AddDays(-7))
                .ToList();

                return View(pastReviews);
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
                ReviewerName = "Uidentfied reviewer",
                ReviewerNames = new string[] { "Dr. Pompus" },
                Comment = input.Comments,
                ScoreCharacter = input.ScoreCharacter,
                ScorePlot = input.ScorePlot,
                ScoreSetting = input.ScoreSetting,
                OwnerUserId = input.Username,
                PublishDate = DateTime.UtcNow
            };

            session.Store(pastReviews);

            session.SaveChanges();

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

    public static class RavenSingleton
    {
        private static IDocumentStore store;

        static RavenSingleton()
        {
            store = new DocumentStore {ConnectionStringName = "RavenDB"}.Initialize();
            IndexCreation.CreateIndexes(typeof(RavenSingleton).Assembly, store);
        }

        public static IDocumentSession GetSession()
        {
            return store.OpenSession();
        }
    }

    public class PastReviewIndex : AbstractIndexCreationTask<PastReviewView>
    {
        public PastReviewIndex()
        {
            Map = docs => from review in docs
                select new { PublishDate = review.PublishDate, OwnerUserId = review.OwnerUserId, NickName = "Sally" };
        }
    }
}
