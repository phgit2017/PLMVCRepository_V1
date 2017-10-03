using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Utilities.CustomAttributes;
using PL.Business.Common;

namespace PL.Business.Dto.IOBalanceV2
{
    public class CustomerDto
    {
        public long CustomerId { get; set; }

        [Required]
        [Display(Name = "Customer Code")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        [StringLength(1000, ErrorMessage = "Up to 1000 characters only.")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        [StringLength(1000, ErrorMessage = "Up to 1000 characters only.")]
        public string CustomerAddress { get; set; }

        public string CustomerDropDownDisplay
        {
            get
            {
                return CustomerCode + " - " + CustomerName;
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

        public bool IsActive { get; set; }
    }
}
