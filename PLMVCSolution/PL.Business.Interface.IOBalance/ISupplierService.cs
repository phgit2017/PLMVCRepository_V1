using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface ISupplierService
    {
        IQueryable<SupplierDto> GetAll();
        SupplierDto FindSupplierById(int supplierId);
        bool SaveSupplier(SupplierDto supplierDetails);
        bool UpdateSupplierDetails(SupplierDto newSupplierDetails);
        bool UpdateInactive(int supplierId, int? updatedBy);
    }
}
