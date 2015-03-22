﻿using System;
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
            var courses = DocumentSession.Query<ClassModel>()
                         .ToList();


            if (courses.FirstOrDefault() != null)
            {
                for (int i = 0; i < courses.Count; i++)
                {
                    if (courses[i].Students != null)
                    {
                        if (courses[i].Students.Contains(User.Identity.GetUserId()))
                        {
                            models.Add(courses[i]);
                        }
                    }
                }
            }
            return View(models);
        }

        public ActionResult StudentAddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentAddClass(StudentAddClass input)
        {
             //Query classes for this confCode and then add student to the list
             //If there is one, then add load that specific object and add the student to the array
            var courses = DocumentSession.Query<ClassModel, StudentAddClassIndex>()
                         .Where(c => c.ConfirmCode == input.classCode)
                         .ToList();

            var student = DocumentSession.Query<StudentModel, AddClassToStudentIndex>()
                          .Where(s => s.StudentID == User.Identity.GetUserId())
                          .ToList();

            if (courses.Count != 0 && student.Count != 0)
            {

                string id = courses[0].Id;
                // Having this Id attribute that gets set by RavenDb 
                // allows for retrieval of the exact object that can be updated or deleted
                // by using the Load command that uses a document Id
                ClassModel course = DocumentSession.Load<ClassModel>(id);
                List<string> list = course.Students.ToList();
                list.Add(User.Identity.GetUserId());
                course.Students = list.ToArray();
                //DocumentSession.SaveChanges();

                string ids = student[0].Id;
                // Having this Id attribute that gets set by RavenDb 
                // allows for retrieval of the exact object that can be updated or deleted
                // by using the Load command that uses a document Id
                StudentModel st = DocumentSession.Load<StudentModel>(ids);
                List<Guid> listS = st.ClassIDs.ToList();
                listS.Add(course.id);
                st.ClassIDs = listS.ToArray();
                
                DocumentSession.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public ActionResult ViewAssignments(Guid classID)
        {
            var assigns = new AssignmentsView();

            var assign = DocumentSession.Query<AssignmentInputView>()
                          .Where(a => a.CourseId == classID && a.DueDate > DateTime.Now)
                          .ToList();

            var submissions = DocumentSession.Query<SubmissionView>()
                               .Where(s => s.StudentId == User.Identity.GetUserId() && s.classId == classID)
                               .ToList();

            if (assign.Count() != 0)
            {
                assigns.Current = assign.ToArray();
            }

            if (submissions.Count != 0)
            {
                assigns.Submitted = submissions.ToArray();
            }

            return View(assigns);
        }

        public ActionResult CurrentAssignment(Guid Id)
        {
            var assignment = DocumentSession.Load<AssignmentInputView>(Id);

            return View(assignment);
        }

        public ActionResult PastSubmission(string submitId)
        {
            //var submission = new List<SubmissionView>();
            //submission.Add(input);

            return View();
        }

        public ActionResult Write(Guid Id)
        {
            // Get SubmissionView that matches this assignment id and user
            var prev = DocumentSession.Query<SubmissionView>()
                        .Where(a => a.StudentId == User.Identity.GetUserId() && a.AssignmentId == Id)
                        .ToList();

            // If first time loading write page, make a StoryInput Model and return it
            if(prev.Count == 0){
                // Load assignmentInputView with this Id
                var assign = DocumentSession.Load<AssignmentInputView>(Id);
                var writeData = new SubmissionView()
                {
                    classId = assign.CourseId,
                    AssignmentId = assign.Id,
                    StudentId = User.Identity.GetUserId(),
                    AssignmentName = assign.AssignmentName,
                    Description = assign.Description,
                    Story = ""
                };
                DocumentSession.Store(writeData);
                DocumentSession.SaveChanges();
                return View(writeData);
            }
            // Else return the SubmissionView from query
            return View(prev[0]);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Write(SubmissionView input)
        {
            // Load the submissionView with the Id of the one from input
            var sv = DocumentSession.Load<SubmissionView>(input.Id);

            // Update the story data
            sv.Story = input.Story;
            // Story and submission date will continue to apdate as long as the 
            // student is making changes before the due date because current assignment will expire at a due date
            sv.SubmissionDate = DateTime.Now;
            //StoryInput story = new StoryInput()
            //{
            // StudentId = User.Identity.GetUserId(),
            // Story = input.Story
            //};

            DocumentSession.Store(sv);
            DocumentSession.SaveChanges();
            //return View();
            return RedirectToAction("Write", new { Id = sv.AssignmentId });
        }

        /*[HttpPost]
        public ActionResult onSubmit(SubmissionView input)
        {
            // Load the submissionView with the Id of the one from input
            var sv = DocumentSession.Load<SubmissionView>(input.Id);

            // Update the story data
            sv.Story = input.Story;

            // Set submission date
            sv.SubmissionDate = new DateTime();

            DocumentSession.Store(sv);
            DocumentSession.SaveChanges();

            return RedirectToAction("ViewAssignments", new { classId = sv.classId });
        }*/

        public ActionResult Train(Guid Id)
        {
            var assign = DocumentSession.Load<AssignmentInputView>(Id);
            return View(assign);
        }

        public ActionResult BrainStorm(Guid Id)
        {
            var assign = DocumentSession.Load<AssignmentInputView>(Id);
            return View(assign);
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