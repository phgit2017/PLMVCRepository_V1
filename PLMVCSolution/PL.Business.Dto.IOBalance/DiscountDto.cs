using PL.Business.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Dto.IOBalance
{
    public class DiscountDto
    {
        public long DiscountID { get; set; }

        [Required]
        [Display(Name = "Discount Percentage")]
        public int DiscountPercentage { get; set; }

        public int? BranchID { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public string CreatedByUserName { get; set; }

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
                    return Convert.ToDateTime(DateCreated).ToString(Globals.DefaultRecordDateTimeFormat);
                }

            }
        }

        public int? UpdatedBy { get; set; }

        public string UpdatedByUserName { get; set; }

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
                    return Convert.ToDateTime(DateUpdated).ToString(Globals.DefaultRecordDateTimeFormat);
                }

            }
        }



        public string BranchName { get; set; }

        public string BranchAddress { get; set; }

        public string BranchDetails { get; set; }
    }
}
