namespace PL.Core.Entity.IOBalanceDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Model
    {
        public Model()
        {
            Products = new HashSet<Product>();
            Models = new HashSet<Model>();
        }

        public int ModelID { get; set; }

        [StringLength(100)]
        public string ModelName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
