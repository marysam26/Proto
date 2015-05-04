using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WriteItUp2.Areas.Teacher.Models;


namespace WriteItUp2.Areas.Student.Models
{

    public class StudentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        // list of classIDs
        public Guid[] ClassIDs { get; set; }
    }

    public class StudentAddClass
    {
        public string classCode { get; set; }
    }

    public class StudentClassModel
    {
        public string TeacherName { get; set; }
        public string ClassName { get; set; }
        public Guid courseId { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class StudentReviewView
    {
        public string Title { get; set; }
        public StoryReviewView ReviewOne { get; set; }
        public StoryReviewView ReviewTwo { get; set; }
    }

    public class SubmitDetails
    {
        // Class ID added for return to assignemtns page without having to go the way back home
        public Guid classId { get; set; }
        public string StoryTitle { get; set; }
        // Im using an html string instead of RegEx removal of formatting in order to keep the student's formatting
        // So that it retains their fonts, colors etc.
        public HtmlString Story { get; set; }
        public string SubmissionId { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public int NumReviews { get; set; }
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
        public DateTime SubmissionDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The story title is required, but may be changed at any time")]
        [Display(Name = "Story Title (Required)")]
        public string StoryTitle { get; set; }

        public string Story { get; set; }
        public int NumReviews { get; set; }
        public string reviewer1 { get; set; }
        public string reviewer2 { get; set; }
    }

    public class StoryReviewView
    {
        // Adding class ID and submission ID for link navigation
        public Guid classId { get; set; }
        public string submitId { get; set; }
        public string AssignmentName { get; set; }
        public int ScoreWhoStory { get; set; }
        public int ScoreWhereStory { get; set; }
        public int ScoreWhenStory { get; set; }
        public int ScoreWhatStory { get; set; }
        public int ScoreWhatNext { get; set; }
        public int ScoreHowStory { get; set; }
        public int ScoreCharacterFeel { get; set; }
        public int ScoreOverall { get; set; }
        public string Comments { get; set; }
        public int reviewNum { get; set; }
    }

    public class AssignmentsView
    {
        public String className { get; set; }
        public AssignmentInputView[] Current { get; set; }
        public SubmissionView[] Submitted { get; set; }
    }

}