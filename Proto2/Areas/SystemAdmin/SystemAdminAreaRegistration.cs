using System.Web.Mvc;

namespace Proto2.Areas.SystemAdmin
{
    public class SystemAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SystemAdmin_default",
                "SystemAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}