using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Proto2.Areas.Teacher.Models;


namespace Proto2.Areas.Student.Models
{

    public class StudentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        // list of classIDs
        public Guid[] ClassIDs { get; set; }
    }

   /* public class VideoView
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }*/

    public class StudentAddClass
    {
        public string classCode { get; set; }
    }

    public class StudentReviewView
    {
        public string Title { get; set; }
        public StoryReviewView ReviewOne { get; set; }
        public StoryReviewView ReviewTwo { get; set; }
    }

    public class SubmitDetails
    {
        public HtmlString Story { get; set; }
        public string SubmissionId { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
    }

    public class SubmissionView
    {
        public string Id { get; set; }
        public Guid classId { get; set; }
        public Guid AssignmentId { get; set; }
        public DateTime DueDate { get; set; }
        public string StudentId { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        // Date is not set until submit is completed, will be empty on all just saves.
        public DateTime SubmissionDate { get; set; }
        public string Story { get; set; }
        public int NumReviews { get; set; }
        public string reviewer1 { get; set; }
        public string reviewer2 { get; set; }
    }

    public class StoryReviewView
    {
        public int ScorePlot { get; set; }
        public int ScoreCharacter { get; set; }
        public int ScoreSetting { get; set; }
        public string Comments { get; set; }
        public int reviewNum { get; set; }
    }

    public class AssignmentsView
    {
        public AssignmentInputView[] Current { get; set; }
        public SubmissionView[] Submitted { get; set; }
    }

    public class BrainStormInput
    {
        //TODO: if we're going to save their picture, we should try
        //to associate it with the model if possible
        
    }
}