using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Core.Entity.ChainSawDBV2;
using PL.Infra.DbContext.Interface;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.Entity;

namespace PL.Infra.DataAccess.ChainSawDBV2
{
    public partial class ChainSawDBV2Entities : System.Data.Entity.DbContext, IChainSawDBContext
    {

        static ChainSawDBV2Entities()
        {
            Database.SetInitializer<ChainSawDBV2Entities>(null);
        }

        public ChainSawDBV2Entities()
            : base("name=ChainSawDBEntities")
        {
        }

        public ChainSawDBV2Entities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public ChainSawDBV2Entities(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        public ChainSawDBV2Entities(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public ChainSawDBV2Entities(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<CustomerPrice> CustomerPrices { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
    }
}
