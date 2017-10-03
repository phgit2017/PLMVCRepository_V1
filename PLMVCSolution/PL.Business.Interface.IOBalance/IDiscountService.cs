using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface IDiscountService
    {
        IQueryable<DiscountDto> GetAll();
        DiscountDto FindDiscountById(long discountId);
        bool SaveDiscount(DiscountDto discountDetails);
        bool UpdateDiscount(DiscountDto discountDetails);
        bool UpdateDiscountActive(DiscountDto discountDetails);
    }
}
