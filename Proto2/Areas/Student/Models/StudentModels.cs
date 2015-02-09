using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proto.Areas.Student.Models
{
    public class VideoView
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }

    public class ReviewView
    {
        public string Title { get; set; }
        public StoryReviewView ReviewOne { get; set; }
        public StoryReviewView ReviewTwo { get; set; }
    }

    public class StoryReviewView
    {
        public int name { get; set; }
        public int ScorePlot { get; set; }
        public int ScoreCharacter { get; set; }
        public int ScoreSetting { get; set; }
        public string Comments { get; set; }
    }

    public class BrainStormInput
    {
        //TODO: if we're going to save their picture, we should try
        //to associate it with the model if possible
        
    }

    public class StoryInput
    {
        public string Title { get; set; }
        public string Story { get; set; }
    }
}