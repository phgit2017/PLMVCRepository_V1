using Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace PL.MVC.IOBalance.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IOBalanceAuthorizeUserAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //bool isAuthorized = base.AuthorizeCore(httpContext);
            //if (isAuthorized)
            //{
            //    return false;
            //}


            //int x = (int)httpContext.Session[SessionVariables.UserIdLoggedIn];
            if ( (httpContext.Session[SessionVariables.UserIdLoggedIn] == null ) || ((int)httpContext.Session[SessionVariables.UserIdLoggedIn]) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            

            //if (Users.Split('.').Contains(httpContext.User.Identity.Name.ToString()))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new RedirectToRouteResult(
            //                       new RouteValueDictionary 
            //                       {
            //                           { "action", "UnAuthorized" },
            //                           { "controller", "Authentication" }
            //                       });

            filterContext.Result = new ViewResult
            {
                ViewName = IOBALANCEMVC.Authentication.Views.UnAuthorized
            };

        }
    }
}