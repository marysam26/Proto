using System;
using System.ComponentModel.DataAnnotations;

namespace Proto.Areas.Reviewer.Models
{
    public class StoryViewModel
    {
        public string Name { get; set; }
        public DateTime DatePublished { get; set; }
        public int NumReviews { get; set; }
    }

    public class ReviewViewModel
    {
        public string Title { get; set; }
        public string Story { get; set; }
    }

    public class ReviewInput
    {
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
        public string Title { get; set; }
        public string Story { get; set; }
        public int ScorePlot { get; set; }
        public int ScoreSetting { get; set; }
        public int ScoreCharacter { get; set; }
        public string Comment { get; set; }
        public string ReviewerName { get; set; }
        public string[] ReviewerNames { get; set; }
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
