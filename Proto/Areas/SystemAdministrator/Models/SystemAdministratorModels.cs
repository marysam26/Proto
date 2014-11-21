using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proto.Areas.SystemAdministrator.Models
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
}
