using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WriteItUp2.Areas;
using WriteItUp2.Areas.Account;
using Raven.Client;
using RavenDB.AspNet.Identity;

namespace WriteItUp2.Controllers
{
    public class HomeController : Controller
    {
        public IDocumentSession DocumentSession { get; set; }
           public HomeController()
        {
            this.UserManager = new UserManager<WriteItUpUser>(
                new UserStore<WriteItUpUser>(() => this.DocumentSession));
        }

        public UserManager<WriteItUpUser> UserManager { get; private set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Teacher()
        {
            if (UserManager.IsInRole(User.Identity.GetUserId(), WriteItUpRoles.Teacher))
            {
                return RedirectToAction("Index", "TeacherHome", new {area = "Teacher"});
            }

            else
            {
                return RedirectToAction("Error", "Home", new {error = "You are not registered as a Teacher and " +
                                                                      "do not have access to the teacher page."});
            }
        }

        public ActionResult Reviewer()
        {
            if (UserManager.IsInRole(User.Identity.GetUserId(), WriteItUpRoles.Reviewer))
            {
                return RedirectToAction("Index", "ReviewerHome", new { area = "Reviewer" });
            }

            else
            {
                return RedirectToAction("Error", "Home", new
                {
                    error = "You are not registered as a Reviewer and " +
                            "do not have access to this page."
                });
            }
        }

        public ActionResult Student()
        {
            if (UserManager.IsInRole(User.Identity.GetUserId(), WriteItUpRoles.Student))
            {
                return RedirectToAction("Index", "StudentHome", new { area = "Student" });
            }

            else
            {
                return RedirectToAction("Error", "Home", new
                {
                    error = "You are not registered as a Student and " +
                            "do not have access to this page."
                });
            }
        }

        public ActionResult SystemAdmin()
        {
            //FOR NOW everyone can access the system admin page just for ease of use
            //if (UserManager.IsInRole(User.Identity.GetUserId(), WriteItUpRoles.SystemAdmin))
            //{
                return RedirectToAction("Index", "SystemAdminHome", new { area = "SystemAdmin" });
            //}

            //else
            //{
            //    return RedirectToAction("Error", "Home", new
            //    {
            //        error = "You are not registered as a System Administrator and " +
            //                "do not have access to the this page."
            //    });
            //}
        }

        public ActionResult Error(string error)
        {
            ViewData.Add("Error", error);
            return View();
        }
    }
}