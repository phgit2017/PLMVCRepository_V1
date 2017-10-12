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
    public class CategoryService : ICategoryService
    {
        #region DeclarationsAndConstructors
        IIOBalanceRepository<Category> _category;

        IOBalanceEntity.Category category;
        public CategoryService(IIOBalanceRepository<Category> category)
        {
            this._category = category;

            this.category = new IOBalanceEntity.Category();
        }
        #endregion DeclarationsAndConstructors

        #region InterfaceImplementations
        public IQueryable<CategoryDto> GetAll()
        {
            var list = from c in _category.GetAll()
                       select new CategoryDto()
                       {
                           CategoryCode = c.CategoryCode.Trim(),
                           CategoryID = c.CategoryID,
                           CategoryName = c.CategoryName.Trim(),
                           CategoryDisplay = c.CategoryCode.Trim() + " - " + c.CategoryName.Trim(),
                           IsActive = c.IsActive,
                           SortOrder = c.SortOrder
                       };

            return list;
        }

        public CategoryDto FindCategoryById(int categoryId)
        {
            var list = GetAll().Where(c => c.CategoryID == categoryId).FirstOrDefault();
            return list;
        }

        public CategoryDto FindCategoryByIdAndActive(int categoryId)
        {
            var list = GetAll().Where(c => c.CategoryID == categoryId && c.IsActive == true).FirstOrDefault();
            return list;
        }

        public bool SaveCategory(CategoryDto categoryDetails)
        {
            this.category = categoryDetails.DtoToEntity();

            if (this._category.Insert(this.category).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateCategoryDetails(CategoryDto newCategoryDetails)
        {
            var oldCategoryDetails = FindCategoryById(newCategoryDetails.CategoryID);
            var updatedCategoryDetails = this.category;
            updatedCategoryDetails = new Category()
            {
                CategoryID = newCategoryDetails.CategoryID,
                CategoryCode = newCategoryDetails.CategoryCode.Trim(),
                CategoryName = newCategoryDetails.CategoryName.Trim(),
                IsActive = oldCategoryDetails.IsActive
            };

            if (this._category.Update2(updatedCategoryDetails).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateInactive(int categoryId)
        {
            var ActiveCategory = FindCategoryById(categoryId);

            if (ActiveCategory == null)
            {
                return false;
            }

            this.category = new Category()
            {
                CategoryID = categoryId,
                CategoryCode = ActiveCategory.CategoryCode,
                CategoryName = ActiveCategory.CategoryName,
                IsActive = ActiveCategory.IsActive ? false : true,
                SortOrder = ActiveCategory.SortOrder
            };


            if (this._category.Update(this.category, c => c.CategoryID == categoryId).IsNull())
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
