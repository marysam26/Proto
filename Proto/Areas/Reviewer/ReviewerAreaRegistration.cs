using System.Web.Mvc;

namespace WriteItUp.Areas.Reviewer
{
    public class ReviewerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reviewer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reviewer_default",
                "Reviewer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
