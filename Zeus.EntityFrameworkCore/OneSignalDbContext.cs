using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Zeus.Core.Models;
using Zeus.EntityFrameworkCore.EntityConfigurations;

namespace Zeus.EntityFrameworkCore
{
    public class OneSignalDbContext : DbContext
    {
        private readonly static string _connectionStringKey = "OneSignalDB";

        public OneSignalDbContext(IConfiguration configuration) : base(GetOptions(configuration))
        {

        }

        private static DbContextOptions GetOptions(IConfiguration configuration)
        {
            // Used for unit tests only
            if (Convert.ToBoolean(configuration["IsUseInMemoryDB"]))
            {
                return new DbContextOptionsBuilder<OneSignalDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging()
                                    .Options;
            }

            var connectionString = configuration.GetConnectionString(_connectionStringKey);
            var sqlConBuilder = new SqlConnectionStringBuilder(connectionString);
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString,
                                                                       sqlServerOptions =>
                                                                       {
                                                                           sqlServerOptions.CommandTimeout(sqlConBuilder.ConnectTimeout);
                                                                           sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

                                                                       }).AddInterceptors(new CommandInterceptor()).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppConfiguration());
        }

        public virtual DbSet<App> Apps { get; set; }

    }
}
