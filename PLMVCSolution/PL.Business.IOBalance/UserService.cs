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
    public class UserService : IUserService
    {
        #region DeclarationAndConstructors
        IIOBalanceRepository<User> _user;
        IIOBalanceRepository<UserType> _userType;

        IOBalanceEntity.User user;
        IOBalanceEntity.UserType userType;
        public UserService(IIOBalanceRepository<User> user,
            IIOBalanceRepository<UserType> userType)
        {
            this._user = user;
            this._userType = userType;


            this.user = new User();
            this.userType = new UserType();
        }
        #endregion DeclarationAndConstructors

        #region InterfaceImplementations
        public IQueryable<UserTypeDto> GetAllUserType()
        {
            var list = from ut in _userType.GetAll()
                       select new UserTypeDto()
                       {
                           UserTypeID = ut.UserTypeID,
                           UserTypeName = ut.UserTypeName
                       };
            return list;
        }

        public IQueryable<UserDto> GetAll()
        {
            var list = from u in _user.GetAll()
                       select new UserDto()
                       {
                           UserID = u.UserID,
                           UserName = u.UserName.Trim(),
                           Password = u.Password,
                           HashPassword = u.HashPassword,
                           CreatedBy = u.CreatedBy,
                           DateCreated = u.DateCreated,
                           DateUpdated = u.DateUpdated,
                           IsActive = u.IsActive,
                           UpdatedBy = u.UpdatedBy,
                           UserTypeID = u.UserTypeID,
                           UserTypeName = u.UserType == null ? string.Empty : u.UserType.UserTypeName,
                           BranchId = u.BranchID,
                           BranchName = u.Branch == null ? string.Empty : u.Branch.BranchName
                       };
            return list;
        }

        public UserDto FindUserByUserId(int userId)
        {
            var list = GetAll().Where(u => u.UserID == userId).FirstOrDefault();
            return list;
        }

        public UserTypeDto FindUserTypeById(int userTypeId)
        {
            var list = GetAllUserType().Where(u => u.UserTypeID == userTypeId).FirstOrDefault();
            return list;
        }

        public bool SaveUser(UserDto dto)
        {
            this.user = dto.DtoToEntity();

            if (this._user.Insert(user).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateUser(int userId, UserDto newUserDetails)
        {
            var oldUserDetails = FindUserByUserId(userId);
            this.user = new User()
            {
                UserID = userId,
                UserName = newUserDetails.UserName.Trim(),
                Password = oldUserDetails.Password,
                HashPassword = oldUserDetails.HashPassword,
                UserTypeID = newUserDetails.UserTypeID,
                IsActive = oldUserDetails.IsActive,
                CreatedBy = oldUserDetails.CreatedBy,
                DateCreated = oldUserDetails.DateCreated,
                UpdatedBy = newUserDetails.UpdatedBy,
                DateUpdated = newUserDetails.DateUpdated,
                BranchID = newUserDetails.BranchId
            };

            if (_user.Update2(this.user).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateInactiveUser(int userId, int? updatedBy)
        {
            var oldUserDetails = FindUserByUserId(userId);
            this.user = new User()
            {
                UserID = userId,
                UserName = oldUserDetails.UserName,
                Password = oldUserDetails.Password,
                HashPassword = oldUserDetails.HashPassword,
                UserTypeID = oldUserDetails.UserTypeID,
                IsActive = oldUserDetails.IsActive ? false : true,
                CreatedBy = oldUserDetails.CreatedBy,
                DateCreated = oldUserDetails.DateCreated,
                UpdatedBy = updatedBy,
                DateUpdated = System.DateTime.Now,
                BranchID = oldUserDetails.BranchId
            };

            if (_user.Update2(this.user).IsNull())
            {
                return false;
            }
            return true;
        }
        #endregion InterfaceImplementations

        #region PrivateMethods

        #endregion PrivateMethods
    }
}
