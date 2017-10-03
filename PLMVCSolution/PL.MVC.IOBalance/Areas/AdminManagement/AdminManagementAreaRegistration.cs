using System.Web.Mvc;

namespace PL.MVC.IOBalance.Areas.AdminManagement
{
    public class AdminManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminManagement_default",
                "AdminManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
