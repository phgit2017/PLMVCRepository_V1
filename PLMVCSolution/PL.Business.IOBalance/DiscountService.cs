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
    public class DiscountService : IDiscountService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<Discount> _discount;

        IUserService _userService;

        IOBalanceEntity.Discount discount;
        public DiscountService(IIOBalanceRepository<Discount> discount,
            IUserService userService)
        {
            this._discount = discount;
            this._userService = userService;
            this.discount = new Discount();

        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementations
        public IQueryable<DiscountDto> GetAll()
        {
            var list = from d in _discount.GetAll()
                       select new DiscountDto()
                       {
                           BranchID = d.BranchID,
                           DiscountPercentage = d.DiscountPercentage,
                           DiscountID = d.DiscountID,
                           CreatedBy = d.CreatedBy,
                           DateCreated = d.DateCreated,
                           DateUpdated = d.DateUpdated,
                           UpdatedBy = d.UpdatedBy,
                           BranchName = d.Branch == null ? string.Empty : d.Branch.BranchName,
                           BranchAddress = d.Branch == null ? string.Empty : d.Branch.BranchAddress,
                           BranchDetails = d.Branch == null ? string.Empty : d.Branch.BranchDetails,
                           IsActive = d.IsActive,

                           CreatedByUserName = d.CreatedBy == null ? string.Empty : d.User_CreatedBy.UserName,
                           UpdatedByUserName = d.UpdatedBy == null ? string.Empty : d.User_UpdatedBy.UserName

                           //CreatedByUserName = d.CreatedBy == null ? string.Empty : (from u in _userService.GetAll().ToList() where u.UserID == u.CreatedBy && u.IsActive select u).FirstOrDefault().UserName,
                           //UpdatedByUserName = d.UpdatedBy == null ? string.Empty : (from u in _userService.GetAll().ToList() where u.UserID == u.UpdatedBy && u.IsActive select u).FirstOrDefault().UserName

                           //CreatedByUserName = d.CreatedBy == null ? string.Empty : _userService.GetAll().ToList().Where(u => u.UserID == d.CreatedBy).FirstOrDefault().UserName,
                           //UpdatedByUserName = d.UpdatedBy == null ? string.Empty : _userService.GetAll().ToList().Where(u => u.UserID == d.UpdatedBy).FirstOrDefault().UserName
                       };


            return list;
        }

        public DiscountDto FindDiscountById(long discountId)
        {
            var list = GetAll().Where(d => d.DiscountID == discountId).FirstOrDefault();
            return list;
        }

        public bool SaveDiscount(DiscountDto discountDetails)
        {
            this.discount = discountDetails.DtoToEntity();

            if (this._discount.Insert(this.discount).IsNull())
            {
                return false;
            }
            return true;
        }

        public bool UpdateDiscount(DiscountDto discountDetails)
        {
            var oldDiscountDetails = FindDiscountById(discountDetails.DiscountID);
            var updatedDiscountDetails = new IOBalanceEntity.Discount()
            {
                BranchID = discountDetails.BranchID,
                CreatedBy = oldDiscountDetails.CreatedBy,
                DateCreated = oldDiscountDetails.DateCreated,
                UpdatedBy = discountDetails.UpdatedBy,
                DateUpdated = System.DateTime.Now,
                DiscountID = oldDiscountDetails.DiscountID,
                DiscountPercentage = discountDetails.DiscountPercentage,
                IsActive = oldDiscountDetails.IsActive
            };

            if (_discount.Update2(updatedDiscountDetails).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateDiscountActive(DiscountDto discountDetails)
        {
            var oldDiscountDetails = FindDiscountById(discountDetails.DiscountID);
            var updatedDiscountDetails = new IOBalanceEntity.Discount()
            {
                BranchID = oldDiscountDetails.BranchID,
                CreatedBy = oldDiscountDetails.CreatedBy,
                DateCreated = oldDiscountDetails.DateCreated,
                UpdatedBy = discountDetails.UpdatedBy,
                DateUpdated = System.DateTime.Now,
                DiscountID = oldDiscountDetails.DiscountID,
                DiscountPercentage = oldDiscountDetails.DiscountPercentage,
                IsActive = oldDiscountDetails.IsActive ? false : true
            };

            if (_discount.Update2(updatedDiscountDetails).IsNull())
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
