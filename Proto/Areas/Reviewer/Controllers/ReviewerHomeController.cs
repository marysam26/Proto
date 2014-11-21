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
            return View();
        }

        public ActionResult PastReviews()
        {
            return View();
        }

        public ActionResult ReviewStory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReviewStory(ReviewInput input)
        {
            return View();
        }

        public ActionResult ViewPastReviews()
        {
            return View();
        }

        public ActionResult Discuss()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Discuss(CommentInput input)
        {
            return View();
        }
    }
}
