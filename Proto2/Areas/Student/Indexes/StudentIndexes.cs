using System.Linq;
using Proto2.Areas.Student.Models;
using Proto2.Areas.Teacher.Models;
using Raven.Client.Indexes;

namespace Proto2.Areas.Student.Indexes
{
    public class ViewClassesIndex : AbstractIndexCreationTask<ClassViewModel>
    {
        public ViewClassesIndex()
        {
            Map = docs => from course in docs
                          select new { className = course.className, teacherID = course.teacherID, Students = course.Students};
        }
    }
}