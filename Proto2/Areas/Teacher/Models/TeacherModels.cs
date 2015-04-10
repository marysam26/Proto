using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WriteItUp2.Areas.Reviewer.Models;
using WriteItUp2.Areas.Student.Models;
using WriteItUp2.Areas.SystemAdmin.Models;

namespace WriteItUp2.Areas.Teacher.Models
{
    public class TeacherModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Guid> Classes { get; set; } 
    }
    public class ClassModel
    {
        public string teacherId { get; set; }
        public Guid Id { get; set; }
        public string ConfirmCode { get; set; }
        public DateTime EndDate { get; set; }
        //List of students
        public List<string> Students { get; set; }
        // List of reviewers
        // When a reviewer agrees to review for this class it adds them to this list
        public List<string> Reviewers { get; set; }
        public string ClassName { get; set; }
    }



    public class StudentViewList
    {
        public List<StudentModel> StudentList { get; set; }
        public Guid CourseId { get; set; }
    }

    public class ReviewerViewList
    {
        public List<ReviewerModel> ReviewerList { get; set; }
        public Guid CourseId { get; set; }
    }

    public class StudentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Add list of classes they are enrolled in, probably only one
        // ClassIDs are not the same as className, so knowing just the ID should be able to retrieve the teacherID
        public string classID { get; set; }
        public int NumReviews { get; set; }
        public string Confirmed { get; set; }
        public string teacherID { get; set; }

    }

    public class ReviewerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string classID { get; set; }
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

    public class AddClassInput
    {
        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [DisplayName("Class Name")]
        public string ClassName { get; set; }

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

    public class AssignmentInput
    {
        public Guid Id { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

    }


    public class AssignmentAddInput
    {
        public List<AssignmentView> Assignments { get; set; }
        public Guid CourseId { get; set; }
    }

    public class AssignmentViewList
    {
        public List<AssignmentInputView> Assignments { get; set; }
        public Guid CourseId { get; set; }
    }

    public class AssignmentInputView
    {
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        [Required]
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        public Guid CourseId { get; set; }
        public Guid Id { get; set; }


    }



}