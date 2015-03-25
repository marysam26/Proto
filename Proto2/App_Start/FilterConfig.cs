using System.Web;
using System.Web.Mvc;
using Proto2.Areas.Account;

namespace Proto2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute
            //{
            //    Roles = ProtoRoles.SystemAdmin + "," + ProtoRoles.Teacher + "," + ProtoRoles.Student
            //            + "," + ProtoRoles.Reviewer
            //});
        }
    }
}
