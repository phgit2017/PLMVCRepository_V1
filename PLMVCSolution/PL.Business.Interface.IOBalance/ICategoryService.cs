using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface ICategoryService
    {
        IQueryable<CategoryDto> GetAll();
        CategoryDto FindCategoryById(int categoryId);
        CategoryDto FindCategoryByIdAndActive(int categoryId);
        bool SaveCategory(CategoryDto categoryDetails);
        bool UpdateCategoryDetails(CategoryDto newCategoryDetails);
        bool UpdateInactive(int categoryId);
    }
}
