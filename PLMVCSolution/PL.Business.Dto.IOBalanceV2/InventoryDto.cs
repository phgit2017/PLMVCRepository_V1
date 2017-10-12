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
}
