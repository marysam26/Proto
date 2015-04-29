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
        public string Id { get; set; }
        public string SubmitId { get; set; }
        public int ScoreWhoStory { get; set; }
        public int ScoreWhereStory { get; set; }
        public int ScoreWhenStory { get; set; }
        public int ScoreWhatStory { get; set; }
        public int ScoreWhatNext { get; set; }
        public int ScoreHowStory { get; set; }
        public int ScoreCharacterFeel { get; set; }
        public int ScoreOverall { get; set; }
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
        public List<SelectListItem> KeyList2 { get; set; }

        [Required]
        [Range(0, 1)]
        [Display(Name = "Who is in the story?")]
        public int ScoreWhoStory { get; set; }

        [Required]
        [Range(0, 1)]
        [Display(Name = "Where does the story take place?")]
        public int ScoreWhereStory { get; set; }

        [Required]
        [Range(0, 1)]
        [Display(Name = "When does the story take place?")]
        public int ScoreWhenStory { get; set; }

        [Required]
        [Range(0, 1)]
        [Display(Name = "What happens in the story?")]
        public int ScoreWhatStory { get; set; }

        [Required]
        [Range(0, 1)]
        [Display(Name = "What happens next?")]
        public int ScoreWhatNext { get; set; }

        [Required]
        [Range(0, 1)]
        [Display(Name = "How does the story end?")]
        public int ScoreHowStory { get; set; }

        [Required]
        [Range(0, 1)]
        [Display(Name = "How does the main character(s) feel?  How does the other character(s) feel?")]
        public int ScoreCharacterFeel { get; set; }

        [Required]
        [Range(0, 7)]
        [Display(Name = "Overall story score.")]
        public int ScoreOverall { get; set; }

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