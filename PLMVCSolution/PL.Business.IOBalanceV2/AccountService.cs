using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.IOBalanceV2;
using PL.Business.Interface.IOBalanceV2;
using PL.Business.IOBalanceV2.Extensions;

//-- Core
using PL.Core.Entity.IOBalanceDBV2;
using PL.Core.Entity.Repository.Interface;
using IOBalanceDBV2Entity = PL.Core.Entity.IOBalanceDBV2;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;
namespace PL.Business.IOBalanceV2
{
    public class AccountService : IAccountService
    {
        #region Declarations and constructors
        IIOBalanceV2Repository<webpages_UsersInRoles> _webpages_UsersInRoles;

        IOBalanceDBV2Entity.webpages_UsersInRoles webpages_UsersInRoles;
        public AccountService(IIOBalanceV2Repository<webpages_UsersInRoles> webpages_UsersInRoles)
        {
            this._webpages_UsersInRoles = webpages_UsersInRoles;
            this.webpages_UsersInRoles = new IOBalanceDBV2Entity.webpages_UsersInRoles();
        }
        #endregion Declarations and constructors

        #region Interface implementations
        public bool SaveUserInRoleDetails(UserInRoleDto newDetails)
        {

            this.webpages_UsersInRoles = newDetails.DtoToEntity();
            if (this._webpages_UsersInRoles.Insert(this.webpages_UsersInRoles).IsNull())
            {
                return false;
            }

            return true;
        }
        #endregion Interface implementations

    }
}
