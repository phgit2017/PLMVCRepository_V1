using PL.Business.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }

        [Display(Name = "Category Code")]
        [Required(ErrorMessage = "Category Code is required")]
        [StringLength(50, ErrorMessage = "Up to 50 characters only.")]
        public string CategoryCode { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(255, ErrorMessage = "Up to 255 characters only.")]
        public string CategoryName { get; set; }

        public string CategoryDisplay { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }
    }
}
