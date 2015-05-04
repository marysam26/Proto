using System.Linq;
using WriteItUp2.Areas.Reviewer.Models;
using Raven.Client.Indexes;

namespace WriteItUp2.Areas.Reviewer.Indexes
{
    public class PastReviewIndex : AbstractIndexCreationTask<PastReviewView>
    {
        public PastReviewIndex()
        {
            Map = docs => from review in docs
                select new { PublishDate = review.PublishDate, StudentId = review.StudentId, NickName = "Sally" };
        }
    }
}