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
        public string StudentID { get; set; }
        public string Name { get; set; }
        // list of classIDs
        public string[] ClassIDs { get; set; }
        public SubmissionView[] Submissions { get; set; }
    }

    public class VideoView
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }

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

    public class SubmissionView
    {
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string StudentId { get; set; }
        public Guid StoryId { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
    }

    public class StoryReviewView
    {
        public int ScorePlot { get; set; }
        public int ScoreCharacter { get; set; }
        public int ScoreSetting { get; set; }
        public string Comments { get; set; }
    }

    public class AssignmentsView
    {
        public AssignmentView[] Current { get; set; }
        public SubmissionView[] Submitted { get; set; }
    }

    public class CurrentAssignments
    {
        public int AssignmentID { get; set; }
        public string CurrentName { get; set; }
        public int CurrentCount { get; set; }
    }

    public class SubmittedAssignments
    {
        public string ID { get; set; }
        public string SubmittedName { get; set; }
        public int SubmittedCount { get; set; }
    }

    public class BrainStormInput
    {
        //TODO: if we're going to save their picture, we should try
        //to associate it with the model if possible
        
    }

    public class StoryInput
    {
        public string StudentId { get; set; }
        public Guid StoryId { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
    }
}