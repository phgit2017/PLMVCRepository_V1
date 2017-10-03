using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Dto.IOBalance
{
    public class UserTypeDto
    {
        public int UserTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserTypeName { get; set; }
    }
}
