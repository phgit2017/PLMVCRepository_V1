using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.IO;
using PL.Business.Common;

namespace PL.Business.Dto.IOBalance
{
    public class ProductEditDto
    {
        public long ProductID { get; set; }

        [Required]
        [Display(Name = "Product Code")]
        [StringLength(30, ErrorMessage = "Up to 30 characters only.")]
        public string EditProductCode { get; set; }

        [StringLength(255, ErrorMessage = "Up to 255 characters only.")]
        public string ProductName { get; set; }

        public string ProductDesc { get; set; }

        [StringLength(100, ErrorMessage = "Up to 100 characters only.")]
        public string ProductExtension { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Quantity { get; set; }

        [StringLength(20, ErrorMessage = "Up to 20 characters only.")]
        public decimal? OriginalPrice { get; set; }

        //[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [StringLength(20, ErrorMessage = "Up to 20 characters only.")]
        public decimal Price { get; set; }

        public int? UnitID { get; set; }

        public string UnitName { get; set; }

        public int? CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string CategoryCode { get; set; }

        public int? SupplierID { get; set; }

        public string SupplierName { get; set; }

        public bool IsActive { get; set; }

        [StringLength(1000, ErrorMessage = "Up to 1000 characters only.")]
        public string Remarks { get; set; }

        [StringLength(300, ErrorMessage = "Up to 300 characters only.")]
        public string BarCode { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        [Required]
        [Display(Name = "Size")]
        [StringLength(20, ErrorMessage = "Up to 20 characters only.")]
        public string Size { get; set; }

        public int? ModelID { get; set; }

        public string Model { get; set; }

        public string ProductImage { get; set; }

        public int? BranchID { get; set; }

        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string BranchDetails { get; set; }



        //Order Detail
        public string OverrideDisplay { get; set; }
        public string OverrideExtDisplay { get; set; }

        //Dropdown
        public string ProductFullDisplay
        {
            get
            {
                string fulldisplay = string.Format("{0} - {1}", EditProductCode, ProductName);
                return fulldisplay;
            }
        }

        public string ProductFullDisplayWithExtension
        {
            get
            {
                string fulldisplay = string.Format("{0} - {1} - {2}", EditProductCode, ProductName, ProductExtension);
                return fulldisplay;
            }
        }

        public string CategoryFullDisplay
        {
            get
            {
                string fulldisplay = string.Format("{0} - {1}", CategoryCode, CategoryName);
                return fulldisplay;
            }
        }

        public string DropDownDisplay
        {
            get
            {
                string display = ProductFullDisplay;
                return display;
            }
        }

        public string ProductImagePath
        {
            get
            {
                var dir = string.Format("/{0}", Constants.ProductImageContentDir);
                var fileName = ProductImage == null ? Constants.DefaultProductImageContent : ProductImage;

                //Production Path
                var path = string.Format("../{0}/{1}", dir, fileName);

                //Local Path
                //var path = string.Format("{0}/{1}", dir, fileName);

                return path;
            }
        }

        public string QtyWithFormat
        {
            get
            {
                return Convert.ToDecimal(Quantity).ToString("0.00");
            }
        }

        public string PriceWithFormat
        {
            get
            {
                return Convert.ToDecimal(Price).ToString("0.00");
            }
        }

        public string OriginalPriceWithFormat
        {
            get
            {
                return Convert.ToDecimal(OriginalPrice).ToString("0.00");
            }
        }

        public string CRType { get; set; }
    }
}
