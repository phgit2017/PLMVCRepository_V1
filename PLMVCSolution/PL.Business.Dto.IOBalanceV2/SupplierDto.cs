using PL.Business.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalanceV2
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }

        [Required]
        [Display(Name = "Supplier Code")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string SupplierCode { get; set; }

        [Display(Name = "Supplier Name")]
        [StringLength(1000, ErrorMessage = "Up to 1000 characters only.")]
        public string SupplierName { get; set; }

        public string SupplierInfoDisplay
        {
            get
            {
                return SupplierCode + " - " + SupplierName;
            }
        }

        public DateTime? DateCreated { get; set; }

        public string DateCreatedWithFormat
        {
            get
            {
                if (DateCreated == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToDateTime(DateCreated).ToString(Globals.DefaultRecordDateFormat);
                }

            }
        }

        public string DateCreatedWithTimeFormat
        {
            get
            {
                if (DateCreated == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToDateTime(DateCreated).ToString(Globals.DefaultRecordDateTimeFormat);
                }

            }
        }

        public int? CreatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        public string DateUpdatedWithFormat
        {
            get
            {
                if (DateUpdated == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToDateTime(DateUpdated).ToString(Globals.DefaultRecordDateFormat);
                }

            }
        }

        public string DateUpdatedWithTimeFormat
        {
            get
            {
                if (DateUpdated == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToDateTime(DateUpdated).ToString(Globals.DefaultRecordDateTimeFormat);
                }

            }
        }

        public int? UpdatedBy { get; set; }
    }
}
