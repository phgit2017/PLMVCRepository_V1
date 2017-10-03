using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Core.Entity.IOBalanceDB;
using PL.Infra.DbContext.Interface;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.Entity;

namespace PL.Infra.DataAccess.IOBalanceDB.Context
{
    public partial class IOBalanceDBEntities : System.Data.Entity.DbContext, IIOBalanceDBContext
    {
        static IOBalanceDBEntities()
        {
            Database.SetInitializer<IOBalanceDBEntities>(null);
        }

        public IOBalanceDBEntities()
            : base("name=IOBalanceDBEntities")
        {
        }

        public IOBalanceDBEntities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public IOBalanceDBEntities(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        public IOBalanceDBEntities(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public IOBalanceDBEntities(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerPrice> CustomerPrices { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductLog> ProductLogs { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<ReportCombination> ReportCombinations { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<BatchProductLog> BatchProductLogs { get; set; }
        public virtual DbSet<BatchSummary> BatchSummaries { get; set; }
        
    }
}
