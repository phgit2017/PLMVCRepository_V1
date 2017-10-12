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

using PL.MVC.IOBalance.Infrastructure;
using Infrastructure.Utilities.Extensions;
using LinqKit;

namespace PL.MVC.IOBalance.Areas.AdminManagement.Controllers
{
    public partial class CustomerPriceController : BaseController
    {
        //
        // GET: /AdminManagement/CustomerPrice/

        public virtual ActionResult Index()
        {
            return View();
        }

    }
}
