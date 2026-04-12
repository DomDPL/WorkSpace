using DPLRef.eCommerce.Common.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DPLRef.eCommerce.Accessors.EntityFramework
{
    internal class eCommerceDbContext : DbContext
    {
        internal static eCommerceDbContext UnitTestContext { get; set; }

        protected IConfigurationRoot Configuration { get; set; }

        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public bool AllowDispose { get; set; } = true;

        // Everyone that uses the eCommerceDbContext will use this 
        // constructor method
        internal static eCommerceDbContext Create(bool allowDispose = true) => UnitTestContext ?? new eCommerceDbContext
        {
            AllowDispose = allowDispose
        };

        public override void Dispose()
        {
            // this is the secret of the wrapper, without this do nothing we won't handle rolling back transactions
            // only dispose if we are allowing it to dispose
            if (AllowDispose)
            {
                base.Dispose();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            base.OnConfiguring(optionsBuilder);

            var connectionString = Config.SqlServerConnectionString;
            if (!string.IsNullOrEmpty(connectionString))
            {
                _ = optionsBuilder.UseSqlServer(connectionString);
            }
            else
            {
                connectionString = Config.SqliteConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string environment variable missing.");
                }

                _ = optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}