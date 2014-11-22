using System.Collections.Generic;
using System.Web.Mvc;
using Proto.Areas.Reviewer.Models;

namespace Proto.Areas.Reviewer.Controllers
{
    public class ReviewerHomeController : Controller
    {
        //
        // GET: /Reviewer/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Train()
        {
            //TODO: Return list of TrainVideoView in view
            return View();
        }

        public ActionResult PastReviews()
        {
            //TODO: Return List of PastReviews in view
            return View();
        }

        public ActionResult ReviewStory()
        {
            //TODO: Return view of a story for review
            return View();
        }

        [HttpPost]
        public ActionResult ReviewStory(ReviewInput input)
        {
            //TODO: This will respond to a fom being completed and will eventually be saved to a database
            return View();
        }

        public ActionResult ViewPastReviews()
        {
            //TODO: return view list of PastReviewViews
            return View();
        }

        public ActionResult Discuss()
        {
            //TODO: return Discussion view
            return View();
        }

        [HttpPost]
        public ActionResult Discuss(CommentInput input)
        {
            //TODO: eventually this will respond to a post of a comment and the comment will be saved to database
            return RedirectToAction("Discuss");
        }
    }
}
