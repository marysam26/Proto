using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WriteItUp.Areas.Teacher.Models
{
 
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumReviews { get; set; }
        public string Confirmed { get; set; }
    }

    public class AddStudentInput
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]   
        public string Email { get; set; }
        public Guid Id { get; set; }
    }

    public class StoryView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Stories { get; set; }
    }

    public class ReviewView
    {
        public Guid Id { get; set; }
        public int ScorePlot { get; set; }
        public int ScoreCharacter { get; set; }
        public int ScoreSetting { get; set; }
        public string Comment { get; set; }
        public string ReviewerName { get; set; }
    }

}
