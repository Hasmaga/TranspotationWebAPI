using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TranspotationAPI.Model;
using TranspotationWebAPI.Model;

namespace TranspotationAPI.DbContexts
{
    // For create database and table for the Model
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        // Create Account Table
        public DbSet<Account> Accounts { get; set; }
        // Create Destinations Table
        public DbSet<Destination> Destinations { get; set; }
        // Create OrderDetails Table
        public DbSet<OrderDetail> OrderDetails { get; set; }
        // Create SitDetails Table
        public DbSet<SitDetail> SitDetails { get; set; }
        // Create TranCompanys Table
        public DbSet<TranCompany> TranCompanys { get; set; }
        // Create Trips Table
        public DbSet<Trip> Trips { get; set; }
    }
}
