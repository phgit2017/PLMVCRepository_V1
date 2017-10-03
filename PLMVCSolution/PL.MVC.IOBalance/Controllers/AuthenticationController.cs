using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Business
using PL.Business.Common;
using PL.Business.Dto.IOBalance;
using PL.Business.Interface.IOBalance;

//MVC
using PL.MVC.IOBalance.Controllers;
using PL.MVC.IOBalance.Areas.AdminManagement.Models;

using Infrastructure.Utilities;
using Infrastructure.Utilities.Extensions;
using LinqKit;

namespace PL.MVC.IOBalance.Controllers
{
    public partial class AuthenticationController : BaseController
    {

        #region DeclarationsAndConstructors
        IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }
        #endregion DeclarationsAndConstructors

        #region ActionMethods
        public virtual ActionResult Index()
        {
            var list = new AuthenticationDto();
            return View(list);
        }

        [HttpPost]
        public virtual ActionResult Login(AuthenticationDto dto)
        {
            int userId = 0;
            

            if (ModelState.IsValid)
            {
                userId = _authenticationService.ValidAuthentication(dto);
                if (userId == 0)
                {
                    ModelState.AddModelError("Password", Messages.AccountUserIncorrect);
                }
                else
                {
                    var authenticationDetails = _authenticationService.FindByUserId(userId);

                    if (authenticationDetails.IsActive)
                    {
                        Session[SessionVariables.UserIdLoggedIn] = userId;
                        Session[SessionVariables.UserDetails] = authenticationDetails;
                        return RedirectToAction(IOBALANCEMVC.Home.Index());
                    }
                    else
                    {
                        ModelState.AddModelError("Password", Messages.AccountUserIsInactive);
                    }

                    
                }
            }
            return View(IOBALANCEMVC.Authentication.Views.Index);
        }

        public virtual ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction(IOBALANCEMVC.Authentication.Index());
        }

        public virtual ActionResult UnAuthorized()
        {
            return View();
        }
        #endregion ActionMethods

        #region PrivateMethods

        #endregion PrivateMethods

        #region Dispose

        #endregion Dispose

    }
}
