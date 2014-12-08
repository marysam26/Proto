using System.Collections.Generic;
using System.Web.Mvc;
using Proto.Areas.Student.Models;

namespace Proto.Areas.Student.Controllers
{
    public class StudentHomeController : Controller
    {
        //
        // GET: /Student/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Train()
        {
            return View();
        }

        public ActionResult BrainStorm()
        {
            return View();
        }

        public ActionResult Write()
        {
            return View();
        }

        public ActionResult Reviews()
        {
            //TODO: this should return a list of ReviewView modles
            //Default review, will pull reviews from database but will use this as default for now.
            var ReviewsList = new List<ReviewView>(){
                new ReviewView(){
                    Title = "The Best Story Ever",
                    ReviewOne = new StoryReviewView(){
                        ScorePlot = 5,
                        ScoreCharacter = 4,
                        ScoreSetting = 5,
                        Comments = "Develop a stronger plot and invest more thought to character development."
                    },
                    //ReviewTwo = new StoryReviewView(){
                    //    ScorePlot = 6,
                    //    ScoreCharacter = 4,
                    //    ScoreSetting = 6,
                    //    Comments = "Further develop the characters of your story."
                    //}
                }
            };
            return View(ReviewsList);
        }

        public ActionResult StoryReview()
        {
            //TODO: this should return a list of StoryReviewViews
            //Default review, will pull reviews from database but will use this as default for now.
            var StoryReviewsList = new List<StoryReviewView>(){
                new StoryReviewView(){
                       ScorePlot = 5,
                       ScoreCharacter = 4,
                       ScoreSetting = 5,
                       Comments = "Develop a stronger plot and invest more thought to character development."
                }
            };
            return View(StoryReviewsList);
        }

    }
}
