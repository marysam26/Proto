using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;
using Raven.Client;
using Microsoft.AspNet.Identity;
using Raven.Client.Document;
using Proto2.Areas.Teacher.Models;
using Proto2.Areas.Reviewer.Models;
using Proto2.Areas.Student.Indexes;
using Proto2.Areas.Student.Models;

namespace Proto2.Areas.Student.Controllers
{
    public class StudentHomeController : Controller
    {
        //This will get set by dependency injection. Look at DependencyResolution\RavenRegistry
        public IDocumentSession DocumentSession { get; set; }    

        //
        // GET: /Student/
        public ActionResult Index()
        {

            var models = new List<ClassModel>();
            // TODO: Need to query database, and only return classes where student ID is in 
            /*var courses = DocumentSession.Query<ClassViewModel, ViewClassesIndex>()
                // How to make it pull based on teacherID?
                               //.Where(User.Identity.GetUserId() in r=> r.Students)// How to pull all classes for this teacher?
                               .ToList();

            return View(courses);*/

            return View(models);
        }

        public ActionResult StudentAddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentAddClass(int confCode)
        {
            int hardcodedIDForTesting = 1234;
            //TODO query classes for this confCode and then add student to the list
            var course = DocumentSession.Load<ClassModel>()
                               .Where(c => c.ConfirmCode == confCode);
            if(course != null){
                // Update class viewmodel that has this confirmation code with StudentID(UserId)
                /*var courseAdd = new ClassViewModel()
                {
                    id = Guid.NewGuid(),
                    className = input.className,
                    teacherID = User.Identity.GetUserId(),
                };*/
                //DocumentSession.Store(courseAdd);
                DocumentSession.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

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
            var ReviewsList = new List<StudentReviewView>(){
                new StudentReviewView(){
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