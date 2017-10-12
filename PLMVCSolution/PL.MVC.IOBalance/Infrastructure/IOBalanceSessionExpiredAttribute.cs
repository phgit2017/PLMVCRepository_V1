using Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.MVC.IOBalance.Infrastructure
{
    public class IOBalanceSessionExpiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            var userSession = ctx.Session[SessionVariables.UserDetails];
            if (userSession == null)
            {
                filterContext.Result = new RedirectResult(IOBALANCEMVC.Shared.Views.SessionExpired);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

    }
}