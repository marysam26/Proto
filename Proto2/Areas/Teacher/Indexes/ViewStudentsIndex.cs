using System.Linq;
using Proto2.Areas.Teacher.Models;
using Raven.Client.Indexes;

namespace Proto2.Areas.Teacher.Indexes
{
    public class ViewStudentsIndex : AbstractIndexCreationTask<StudentViewModel>
    {
        public ViewStudentsIndex()
        {
            Map = docs => from student in docs
                          select new { Name = student.Name, classID = student.classID};
        }
    }

    public class ViewClassesIndex : AbstractIndexCreationTask<ClassViewModel>
    {
        public ViewClassesIndex()
        {
            Map = docs => from course in docs
                          select new { className = course.className, teacherID = course.teacherID};
        }
    }
}