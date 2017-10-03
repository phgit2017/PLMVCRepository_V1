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
    public class CategoryService : ICategoryService
    {
        #region Declarations And Constructors
        IIOBalanceV2Repository<Category> _category;

        IOBalanceDBV2Entity.Category category;
        public CategoryService(IIOBalanceV2Repository<Category> category)
        {
            this._category = category;
            this.category = new IOBalanceDBV2Entity.Category();
        }
        #endregion Declarations And Constructors

        #region Interface Implementations
        public IQueryable<CategoryDto> GetAll()
        {
            var result = from det in this._category.GetAll()
                         select new CategoryDto()
                         {
                             CategoryId = det.CategoryID,
                             CategoryName = det.CategoryName
                         };

            return result;
        }

        public CategoryDto FindById(int categoryId)
        {
            var results = GetAll().Where(c => c.CategoryId == categoryId).FirstOrDefault();
            return results;
        }

        public bool SaveDetails(CategoryDto newDetails)
        {
            this.category = newDetails.DtoToEntity();

            if (this._category.Insert(this.category).IsNull())
            {
                return false;
            }

            return true;
        }

        public bool UpdateDetails(CategoryDto newDetails)
        {
            //var oldDetails = FindById(newDetails.CustomerId);
            var details = newDetails.DtoToEntity();

            if (_category.Update2(details).IsNull())
            {
                return false;
            }


            return true;

        }
        #endregion Interface Implementations

    }
}
