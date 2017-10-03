using System.Web.Mvc;

namespace PL.MVC.IOBalance.Areas.TemplateOnly
{
    public class TemplateOnlyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TemplateOnly";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TemplateOnly_default",
                "TemplateOnly/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
