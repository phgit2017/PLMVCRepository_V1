namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int UserTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserTypeName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
