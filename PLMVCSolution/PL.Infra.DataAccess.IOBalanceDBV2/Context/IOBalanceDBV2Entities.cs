using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Core.Entity.IOBalanceDBV2;
using PL.Infra.DbContext.Interface;

using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.Entity;

namespace PL.Infra.DataAccess.IOBalanceDBV2.Context
{
    public partial class IOBalanceDBV2Entities : System.Data.Entity.DbContext, IIOBalanceDBV2Context
    {

        static IOBalanceDBV2Entities()
        {
            Database.SetInitializer<IOBalanceDBV2Entities>(null);
        }

        public IOBalanceDBV2Entities()
            : base("name=IOBalanceDBV2Entities")
        {
        }

        public IOBalanceDBV2Entities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public IOBalanceDBV2Entities(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        public IOBalanceDBV2Entities(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public IOBalanceDBV2Entities(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<QuantityUnit> QuantityUnits { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual DbSet<CustomerPrice> CustomerPrices { get; set; }
    }
}
