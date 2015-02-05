using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Proto.Areas.Teacher.Models;


namespace Proto.Areas.Teacher.Controllers
{
    public class TeacherHomeController : Controller
    {
        //
        // GET: /Teacher/

        public ActionResult Index()
        {
            var models = new List<StudentViewModel>();
            return View(models);
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(AddStudentInput input)
        {
            var student = new StudentViewModel()
            {
                Confirmed = "Not Confirmed",
                Id = new Guid("ED88C65A-77D4-4C57-976D-7E3E5303E6CA"),
                Name = input.FirstName + " " + input.LastName,
                NumReviews = 0
            };
            var students = new List<StudentViewModel> {student};
            return View("Index", students);

        }

        public ActionResult ViewStory(Guid studentId)
        {
            var model = new List<StoryView>
            {
                new StoryView()
                {
                    Title = "Best Story Ever Written",
                    Stories ="This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title This is the greatest story I've ever read in my life. Can't you tell by the title",
                    Id = Guid.NewGuid()
                },
                new StoryView()
                {
                    Title ="Worst Story Ever",
                    Stories="Boo",
                    Id = Guid.NewGuid()
                }
            };
            return View(model);
        }


        public ActionResult ViewReviews(Guid studentid)
        {
            var model = new List<ReviewView>
            {
                new ReviewView()
                {
                    ReviewerName = "Best Reviewer Ever",
                    Comment = "This was the greatest thing I've ever read",
                    ScoreCharacter = 6,
                    ScorePlot = 7,
                    ScoreSetting = 0,
                    Id = Guid.NewGuid()
                }
            };
            return View(model);
        }
    }

}
