using System;
using System.Collections.Generic;
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
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Stories { get; set; }
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
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public bool InUse { get; set; }
    }

    public class AssignmentInput
    {
        //Model for generating a new assignment. Assignments are recognized by their guid generated at time of createion and stored in RavenDB
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Name of Assignment")]
        public string AssignmentName { get; set; }

        [Required]
        [DisplayName("Description of Assignment")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Link for Stratagy Video")]
        public string Link { get; set; }

    }

    public class AssignmentView
    {
        public Guid Id { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
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

}