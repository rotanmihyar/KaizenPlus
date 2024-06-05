using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using kaizenplus.Domain.Roles;
using kaizenplus.Domain.UserRoles;
using kaizenplus.Domain.Users;
using Polly;
using kaizenplus.Domain.Warehouses;
using kaizenplus.Domain.Countries;
using kaizenplus.Domain.WarehouseItems;

namespace kaizenplus.DataAccess
{

    public class DatabaseService : DbContext
    {
        public DbSet<WarehouseItem> WarehouseItem { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<User> Users { get; set; }
       
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles
        {
            get; set;
        }
        
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {

        }

        /// <summary>
        /// Apply database migration if any
        /// </summary>
        public void Migrate()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder
                .SeedRoles()
                .SeedSystemConfig()
                .SeedAdminUser()
                .SeedCountry();

            base.OnModelCreating(builder);
        }

    }
}