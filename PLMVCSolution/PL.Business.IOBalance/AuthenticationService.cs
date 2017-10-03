using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-- Business
using PL.Business.Common;
using PL.Business.Dto.IOBalance;
using PL.Business.Interface.IOBalance;
using PL.Business.IOBalance.Extensions;
//-- Core
using PL.Core.Entity.IOBalanceDB;
using PL.Core.Entity.Repository.Interface;
using IOBalanceEntity = PL.Core.Entity.IOBalanceDB;

//-- Infra
using PL.Infra.DbContext.Interface;

//-- Infrastructure Utilities
using Infrastructure.Utilities.Extensions;

namespace PL.Business.IOBalance
{
    public class AuthenticationService : IAuthenticationService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<User> _user;
        public AuthenticationService(IIOBalanceRepository<User> user)
        {
            this._user = user;
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementations
        public int ValidAuthentication(AuthenticationDto dto)
        {
            var list = GetAll().Where(u => u.UserName == dto.UserName && u.Password == dto.Password);
            if (list.Count() == 0)
            {
                return 0;
            }
            else
            {
                return list.FirstOrDefault().UserID;
            }

        }

        public IQueryable<AuthenticationDto> GetAll()
        {
            var list = from u in _user.GetAll()
                       select new AuthenticationDto()
                       {
                           UserID = u.UserID,
                           Password = u.Password,
                           HashPassword = u.HashPassword,
                           IsActive = u.IsActive,
                           UserName = u.UserName,
                           UserTypeID = u.UserTypeID,
                           UserTypeName = u.UserType == null ? string.Empty : u.UserType.UserTypeName,
                           CreatedBy = u.CreatedBy,
                           DateCreated = u.DateCreated,
                           DateUpdated = u.DateUpdated,
                           UpdatedBy = u.UpdatedBy,
                           BranchId = u.BranchID,
                           BranchName = u.Branch == null ? string.Empty : u.Branch.BranchName
                       };

            return list;
        }

        public AuthenticationDto FindByUserId(int userId)
        {
            var list = GetAll().Where(u => u.UserID == userId).FirstOrDefault();
            return list;
        }
        #endregion InterfaceImplementations

        #region PrivateMethods

        #endregion PrivateMethods


    }
}
