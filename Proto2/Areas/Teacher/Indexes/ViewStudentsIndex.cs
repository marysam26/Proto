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
                          select new { Name = student.Name };
        }
    }
}