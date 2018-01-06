using PL.Business.Dto.IOBalanceV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Interface.IOBalanceV2
{
    public interface IAccountService
    {
        bool SaveUserInRoleDetails(UserInRoleDto newDetails);
    }
}
