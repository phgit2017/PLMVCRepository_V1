using PL.Business.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalanceV2
{
    public class InventoryDto
    {
        public InventoryDto()
        {
            products = new List<ProductDto>();
        }
        public List<ProductDto> products { get; set; }
    }

    public class ProductDto
    {
        public long ProductId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? QuantityUnitId { get; set; }

        public string UnitName { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductCode { get; set; }

        [StringLength(255)]
        public string ProductName { get; set; }

        [StringLength(255)]
        public string ProductDescription { get; set; }

        [StringLength(20)]
        public string ProductSize { get; set; }

        [StringLength(50)]
        public string CurrentNum { get; set; }

        [StringLength(200)]
        public string DRNum { get; set; }

        [StringLength(20)]
        public string CartonNum { get; set; }

        public string ProductDropDownDisplay
        {
            get
            {
                return ProductCode + " - " + ProductName;
            }
        }

        public string ProductInfoDisplay
        {
            get
            {
                return CategoryName + " - " + ProductCode + " - " + ProductName;
            }
        }

        [Required]
        [Display(Name = "Quantity")]
        public decimal? Quantity { get; set; }

        public int? SupplierId { get; set; }
    }

    public class EditProductDto
    {

        public long ProductId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? QuantityUnitId { get; set; }

        public string UnitName { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductCode { get; set; }

        [StringLength(255)]
        public string ProductName { get; set; }

        [StringLength(255)]
        public string ProductDescription { get; set; }

        [StringLength(20)]
        public string ProductSize { get; set; }

        [StringLength(50)]
        public string CurrentNum { get; set; }

        [StringLength(200)]
        public string DRNum { get; set; }

        [StringLength(20)]
        public string CartonNum { get; set; }


        public decimal? Quantity { get; set; }
    }

    public class EditQtyProductDto
    {

        public long ProductId { get; set; }

        public decimal? OldQuantity { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public decimal? EditQuantity { get; set; }

        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }


        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? QuantityUnitId { get; set; }

        public string UnitName { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductCode { get; set; }

        [StringLength(255)]
        public string ProductName { get; set; }

        [StringLength(255)]
        public string ProductDescription { get; set; }

        [StringLength(20)]
        public string ProductSize { get; set; }

        [StringLength(50)]
        public string CurrentNum { get; set; }

        [StringLength(200)]
        public string DRNum { get; set; }

        [StringLength(20)]
        public string CartonNum { get; set; }

    }

    public class BatchInventoryLogDto
    {
        public long BatchInventoryId { get; set; }

        [StringLength(50)]
        public string FileName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }
    }

    public class BatchInventoryResult
    {
        public string STATUSMESSAGE { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PLUS_MINUS { get; set; }
        public string QUANTITY { get; set; }
        public string IS_NEW { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public string UNIT_NAME { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PRODUCT_DESC { get; set; }
        public string PRODUCT_SIZE { get; set; }
        public string CURRENT_NUM { get; set; }
        public string DRNUM { get; set; }
        public string CARTON_NUM { get; set; }
    }

    public class InventoryReportDto
    {
        public long ProductId { get; set; }
        public string ProductDisplay { get; set; }
        public decimal? OldQuantity { get; set; }
        public string Plus { get; set; }
        public string Minus { get; set; }
        public decimal? NewQuantity { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string SupplierDisplay { get; set; }
        public string CustomerDisplay { get; set; }

        public string TransactionDateWithFormat
        {
            get
            {
                if (TransactionDate == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToDateTime(TransactionDate).ToString(Globals.DefaultRecordDateFormat);
                }

            }
        }
    }
}
