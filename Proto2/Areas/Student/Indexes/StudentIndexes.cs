using System.Linq;
using WriteItUp2.Areas.Student.Models;
using WriteItUp2.Areas.Teacher.Models;
using Raven.Client.Indexes;

namespace WriteItUp2.Areas.Student.Indexes
{
    public class ViewClassesIndex : AbstractIndexCreationTask<ClassModel>
    {
        public ViewClassesIndex()
        {
            Map = docs => from course in docs
                          select new { ClassName = course.ClassName, Students = course.Students};
        }
    }

    public class StudentAddClassIndex : AbstractIndexCreationTask<ClassModel>
    {
        public StudentAddClassIndex()
        {
            Map = docs => from course in docs
                          select new { ConfirmCode = course.ConfirmCode };
        }
    }
    //I don't think we need this any more
    //public class AddClassToStudentIndex : AbstractIndexCreationTask<StudentModel>
    //{
    //    public AddClassToStudentIndex()
    //    {
    //        Map = docs => from student in docs
    //                      select new { StudentID = student.StudentID };
    //    }
    //}
}