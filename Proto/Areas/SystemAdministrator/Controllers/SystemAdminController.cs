using System;
using System.Web.Mvc;
using Proto.Areas.SystemAdministrator.Models;

namespace Proto.Areas.SystemAdministrator.Controllers
{
    public class SystemAdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Students()
        {
            return View();
        }

        public ActionResult Teachers()
        {
            return View();
        }

        public ActionResult Reviewers()
        {
            return View();
        }

        public ActionResult ViewStudentsByTeacher()
        {
            return View();
        }

        public ActionResult ConfirmTeacher(Guid id)
        {
            return RedirectToAction("Teachers");
        }

        public ActionResult DeleteTeacher(Guid id)
        {
            return RedirectToAction("Teachers");
        }

        public ActionResult ViewReviewsByReviewers(Guid id)
        {
            return View();
        }

        public ActionResult ConfirmReviewer(Guid id)
        {
            return RedirectToAction("Reviewers");
        }

        public ActionResult DeleteReviewer(Guid id)
        {
            return RedirectToAction("Reviewers");
        }
        public ActionResult EditReviewerVideos()
        {
            return View();
        }

        public ActionResult AddVideo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVideo(AddVideoInput input)
        {
            return RedirectToAction("EditStudentVideos");
        }

        public ActionResult DeleteVideo(Guid id)
        {
            return RedirectToAction("EditStudentVideos");
        }

        public ActionResult ViewStoriesByStudent(Guid id)
        {
            return View();
        }

        public ActionResult ConfirmStudent(Guid id)
        {
            return RedirectToAction("Students");
        }

        public ActionResult DeleteStudent(Guid id)
        {
            return RedirectToAction("Students");
        }

        public ActionResult EditStudentVideos()
        {
            return View();
        }

        //public ActionResult AddStudentVideo()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddStudentVideo(AddVideoInput input)
        //{
        //    return RedirectToAction("EditStudentVideos");
        //}

        //public ActionResult DeleteStudentVideo()
        //{
        //    return RedirectToAction("EditStudentVideos");
        //}
    }
}