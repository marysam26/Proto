using System.Web;
using System.Web.Mvc;
using WriteItUp2.Areas.Account;

namespace WriteItUp2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute
            //{
            //    Roles = WriteItUpRoles.SystemAdmin + "," + WriteItUpRoles.Teacher + "," + WriteItUpRoles.Student
            //            + "," + WriteItUpRoles.Reviewer
            //});
        }
    }
}
