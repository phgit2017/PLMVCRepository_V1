using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Utilities.CustomAttributes;
using PL.Business.Common;

namespace PL.Business.Dto.IOBalance
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Customer Code")]
        [StringLength(10, ErrorMessage = "Up to 10 characters only.")]
        public string CustomerCode { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string MiddleName { get; set; }

        [Display(Name = "Extension")]
        [StringLength(5, ErrorMessage = "Up to 5 characters only.")]
        public string Extension { get; set; }

        //[DateValidation]
        public DateTime? BirthDate { get; set; }

        public string BirthDateWithFormat
        {
            get
            {
                if (BirthDate == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToDateTime(BirthDate).ToString(Globals.DefaultRecordDateFormat);
                }

            }
        }

        [Display(Name = "Address")]
        [StringLength(1000, ErrorMessage = "Up to 1000 characters only.")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [StringLength(100, ErrorMessage = "Up to 100 characters only.")]
        public string City { get; set; }

        [Display(Name = "Region")]
        [StringLength(100, ErrorMessage = "Up to 100 characters only.")]
        public string Region { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(20, ErrorMessage = "Up to 20 characters only.")]
        public string ZipCode { get; set; }

        public bool IsActive { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }


        public string FullName
        {
            get
            {
                string fullname = string.Format("{0}, {1} {2}", LastName, FirstName, MiddleName);
                return fullname;
            }
        }

        public string DropdownDisplay
        {
            get
            {
                string display = string.Format("{0} - {1}, {2} {3}", CustomerCode, LastName, FirstName, MiddleName);
                return display;
            }
        }


        //Customer Price

    }
}
