using System.Web.Mvc;

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
            return View();
        }

        public ActionResult StoryReview()
        {
            //TODO: this should return a list of StoryReviewViews
            return View();
        }
    }
}
