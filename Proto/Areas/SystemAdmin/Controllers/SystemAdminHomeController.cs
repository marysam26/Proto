using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Proto.Areas.SystemAdmin.Models;

namespace Proto.Areas.SystemAdmin.Controllers
{
    public class SystemAdminHomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Students()
        {
            var students = new List<StudentView>();
            return View(students);
        }

        public ActionResult Teachers()
        {
            var teachers = new List<TeacherView>();
            return View(teachers);
        }

        public ActionResult Reviewers()
        {
            var reviewers = new List<ReviewerView>();
            return View(reviewers);
        }

        public ActionResult ViewStudentsByTeacher()
        {
            var students = new List<StudentView>();
            return View(students);
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
            var reviews = new List<ReviewsView>();
            return View(reviews);
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
            var videos = new List<VideoView>();
            return View(videos);
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
            var stories = new List<StoriesView>();
            return View(stories);
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
            var videos = new List<VideoView>();
            return View(videos);
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