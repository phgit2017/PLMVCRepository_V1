using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface IInventoryService
    {
        IQueryable<ProductDto> GetAll();
        ProductDto FindByProductId(long productId);
        long SaveProduct(ProductDto dto);
        bool UpdateProductQty(long productId, ProductDto updatedProductDetails);
        bool UpdateProductDetails(long productId, ProductDto updatedProductDetails);
        bool UpdateProductQtyOrder(long productId, ProductDto updatedProductDetails, string orderTypeAbbrev);
        bool AddQty(ProductDto dto);
        bool VoidQty(ProductDto dto);
        bool DeactivateProduct(int productId);
        bool UpdateProductImage(long productId, ProductDto updatedProductDetails);
        int SaveBulkProducts(BatchSummariesDto dto, List<BatchProductLogDto> dtoList);
        IQueryable<BatchSummariesDto> GetAllBatchSummaries();
    }
}
