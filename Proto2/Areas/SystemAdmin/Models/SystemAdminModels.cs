using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proto2.Areas.SystemAdmin.Models
{
    public class TeacherView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Confirmed { get; set; }
    }

    public class StudentView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Teacher { get; set; }
        public bool Confirmed { get; set; }
    }

    public class ReviewerView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Confirmed { get; set; }
    }

    public class VideoView
    {
        public string Title { get; set; }
        public string Link { get; set; }

    }

    public class AddVideoInput
    {
        [Required]
        [DisplayName("Title of Video")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Link for Video")]
        public string Link { get; set; }
    }

    public class ReviewsView
    {
        //TODO: create a model to hold the information necessary for displaying reviews
        public string Title { get; set; }
        public StoryReviewsView ReviewOne { get; set; }
        public StoryReviewsView ReviewTwo { get; set; }

    }

    public class StoryView
    {
        //TODO: create a model to hold the information necessary for displaying stories
        public string Author { get; set; }
        public StoriesView StoryOne { get; set; }
        public StoriesView StoryTwo { get; set; }
    }

    public class StoriesView
    {
        public string Title { get; set; }
        public string Story { get; set; }
        public string Author { get; set; }
    }

    public class StoryReviewsView
    {
        public int ScorePlot { get; set; }
        public int ScoreCharacter { get; set; }
        public int ScoreSetting { get; set; }
        public string Comments { get; set; }
    }

    public class AddPassView
    {
        public int PassCode { get; set; }
    }
}