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
    public class BranchService : IBranchService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<Branch> _branch;

        IOBalanceEntity.Branch branch;
        public BranchService(IIOBalanceRepository<Branch> branch)
        {
            this._branch = branch;
            this.branch = new IOBalanceEntity.Branch();
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementations
        public IQueryable<BranchDto> GetAll()
        {
            var list = from b in _branch.GetAll()
                       select new BranchDto()
                       {
                           BranchId = b.BranchID,
                           BranchName = b.BranchName,
                           BranchDetails = b.BranchDetails,
                           BranchAddress = b.BranchAddress,
                           CreatedBy = b.CreatedBy,
                           CreatedDate = b.CreatedDate,
                           DateUpdated = b.DateUpdated,
                           UpdatedBy = b.UpdatedBy
                       };

            return list;
        }

        public BranchDto FindBranchById(int branchId)
        {
            var list = GetAll().Where(b => b.BranchId == branchId).FirstOrDefault();
            return list;
        }

        public bool SaveBranch(BranchDto branchDetails)
        {
            this.branch = branchDetails.DtoToEntity();

            if (this._branch.Insert(this.branch).IsNull())
            {
                return false;
            }
            return true;
        }

        public bool UpdateBranch(BranchDto newBranchDetails)
        {
            var oldBranchDetails = FindBranchById(newBranchDetails.BranchId);
            var updatedBranchDetails = newBranchDetails.DtoToEntity();

            updatedBranchDetails.BranchID = newBranchDetails.BranchId;
            updatedBranchDetails.DateUpdated = System.DateTime.Now;

            updatedBranchDetails.CreatedBy = oldBranchDetails.CreatedBy;
            updatedBranchDetails.CreatedDate = oldBranchDetails.CreatedDate;

            //updatedBranchDetails = new Branch()
            //{
            //    CreatedBy = oldBranchDetails.CreatedBy,
            //    CreatedDate = oldBranchDetails.CreatedDate,
            //    BranchID = newBranchDetails.BranchId
            //};

            //BranchName = newBranchDetails.BranchName.ToUpper(),
            //    BranchAddress = newBranchDetails.BranchAddress,
            //    BranchDetails = newBranchDetails.BranchDetails,
            //    UpdatedBy = newBranchDetails.UpdatedBy,
            //    DateUpdated = System.DateTime.Now,

            if (this._branch.Update2(updatedBranchDetails).IsNull())
            {
                return false;
            }


            return true;
        }
        #endregion InterfaceImplementations
    }
}

        