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

        public ActionResult Reviews()
        {
            throw new System.NotImplementedException();
        }

    }
}
