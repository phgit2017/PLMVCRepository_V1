using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalanceV2
{
    public class AccountDto
    {
    }

    public class RoleDto
    { }

    public class UserInRoleDto
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
