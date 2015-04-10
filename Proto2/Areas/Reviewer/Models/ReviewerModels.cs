using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace WriteItUp2.Areas.Reviewer.Models
{
    public class ReviewerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        // list of classIDs
        public List<Guid> ClassIDs { get; set; }
        public List<PastReviewView> Reviews { get; set; }
    }
    public class ViewModel
    {
        
        public Guid ClassIDs { get; set; }
        public string ClassName { get; set; }
        public int NumReviews { get; set; }
    }

    public class ReviewViewModel
    {
        public string Title { get; set; }
        public string Story { get; set; }
        public DateTime DatePublished { get; set; }
        public string Name { get; set; }
        public int NumReviews { get; set; }

    }
    public class Reviews
    {
        public string SubmissionId { get; set; }
        public string AssignmentName { get; set; }
        public DateTime DatePublished { get; set; }
        public int NumReviews { get; set; }
        public string StoryTitle { get; set; }
    }

    public class ReviewerAddClass
    {
        [Required]
        [Range(1000, 9999)]
        [Display(Name = "Class Code")]
        public string classCode { get; set; }
    }

    public class RegisterTeacher
    {
        [Required]
        [Range(1000, 9999)]
        [Display(Name = "Teacher Code")]
        public int TeacherCode { get; set; }
    }
    public class ReviewInputDatabase
    {
        public string SubmitId { get; set; }
        public int ScorePlot { get; set; }
        public int ScoreCharacter { get; set; }
        public int ScoreSetting { get; set; }
        public string Comments { get; set; }
        public string Username { get; set; }

    }

    public class ReviewInput
    {
        public string SubmitId { get; set; }
        public HtmlString Story { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public string StoryTitle { get; set; }

        public List<SelectListItem> KeyList { get; set; }

        [Required]
        [Range(0, 7)]
        [Display(Name = "Score for plot")]
        public int ScorePlot { get; set; }

        [Required]
        [Range(0, 7)]
        [Display(Name = "Score for character")]
        public int ScoreCharacter { get; set; }

        [Required]
        [Range(0, 7)]
        [Display(Name = "Score for setting")]
        public int ScoreSetting { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Score for plot")]
        public string Comments { get; set; }

        [Required]
        public string Username { get; set; }
    }

    public class TrainVideoView
    {
        public string Link { get; set; }
        public string Title { get; set; }
    }

    public class PastReviewHome
    {
        public string Title { get; set; }
        public DateTime DateReviewer { get; set; }

    }

    public class PastReviewView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public int ScorePlot { get; set; }
        public int ScoreSetting { get; set; }
        public int ScoreCharacter { get; set; }
        public string Comment { get; set; }
       // public string ReviewerName { get; set; }
       //public string[] ReviewerNames { get; set; }
        public string StudentId { get; set; }
        public DateTime PublishDate { get; set; }
    }

    public class DiscussionView
    {
        public string Title { get; set; }
        public int ScorePlot { get; set; }
        public int ScoreSetting { get; set; }
        public int ScoreCharacter { get; set; }

        //We'll need to think of a better way to implement this
        public string[][] Dicussion { get; set; }
    }

    public class CommentInput
    {
        public string Comment { get; set; }
    }
}