using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Proto.Areas.SystemAdmin.Models;

namespace Proto.Areas.SystemAdmin.Controllers
{
    public class SystemAdminHomeController : Controller
    {

        //TODO: Feel free to add 'fake data' anywhere an empty
        //list is being returned see Students below for an example
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Students()
        {
            var students = new List<StudentView>()
            {
                new StudentView()
                {
                    Name = "Alan Turing",
                    Email = "turing@email.com",
                    Confirmed = true,
                    Teacher = "Dr. Mocas"
                }
            };
            return View(students);
        }

        public ActionResult Teachers()
        {
            var teachers = new List<TeacherView>()
            {
                new TeacherView()   
                {
                    Name = "Dr. Mocas",
                    Email = "mocas@email.com",
                    Confirmed = true,
                }
            };
            return View(teachers);
        }

        public ActionResult Reviewers()
        {
            var reviewers = new List<ReviewerView>()
            {
                new ReviewerView()
                {
                    Name = "The Best Reviewer",
                    Email = "reviewer@email.com",
                    Confirmed = true
                }
            };
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

        public ActionResult AddReviewerVideo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddReviewerVideo(AddVideoInput input)
        {
            var video = new VideoView()
            {
               Link = input.Link,
               Title = input.Title
            };
            var videos = new List<VideoView> { video };
            return View("EditReviewerVideos", videos);
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

        public ActionResult AddStudentVideo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudentVideo(AddVideoInput input)
        {
            var video = new VideoView()
            {
                Link = input.Link,
                Title = input.Title
            };
            var videos = new List<VideoView> { video };
            return View("EditStudentVideos", videos);
        }

     

        //public ActionResult DeleteStudentVideo()
        //{
        //    return RedirectToAction("EditStudentVideos");
        //}
    }
}