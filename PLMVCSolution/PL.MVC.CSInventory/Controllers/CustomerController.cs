using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


//Business
using PL.Business.Common;
using PL.Business.Dto.ChainSaw;
using PL.Business.Interface.ChainSaw;

//MVC
using PL.MVC.CSInventory.Models;

using PL.MVC.CSInventory.Infrastructure;
using Infrastructure.Utilities.Extensions;
using Infrastructure.Utilities;
using System.Web.Http;

//Kendo
//using Kendo.Mvc.UI;
//using Kendo.Mvc.Extensions;

namespace PL.MVC.CSInventory.Controllers
{
    [RoutePrefix("maintenancecustomer")]
    public class CustomerController : BaseApiController
    {

        #region Declarations and Constructors
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        #endregion Declarations and Constructors

        #region Public methods
        [HttpPost]
        [Route("SaveCustomer")]
        public IHttpActionResult SaveCustomer(CustomerDetailsDto dto)
        {
            dto.CustomerId = 0;
            dto.DateCreated = DateTime.Now;
            dto.CreatedBy = 1;

            var isSuccess = _customerService.SaveDetails(dto);
            return Ok(isSuccess);
        }
        #endregion Public methods

        #region Private methods

        #endregion Private methods
    }
}