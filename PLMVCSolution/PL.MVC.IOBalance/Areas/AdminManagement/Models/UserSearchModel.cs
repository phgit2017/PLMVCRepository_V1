using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.IOBalance.Areas.AdminManagement.Models
{
    public class UserSearchModel
    {
        public string UserName { get; set; }
        public int? UserTypeId { get; set; }
        public string isActive { get; set; }
        public int? BranchId { get; set; }
    }
}