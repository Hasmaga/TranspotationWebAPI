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
        public DbSet<Account> Account { get; set; }
        // Create Depart Table
        public DbSet<Depart> Depart { get; set; }
        // Create Destination Table
        public DbSet<Destination> Destination { get; set; }
        // Create Order Table
        public DbSet<Order> Order { get; set; }
        // Create Role Table
        public DbSet<Role> Role { get; set; }
        // Create SitDetail Table
        public DbSet<SitDetail> SitDetail { get; set; }
        // Create TranCompany Table
        public DbSet<TranCompany> TranCompany { get; set; }
        // Create Trip Table
        public DbSet<Trip> Trip { get; set; }

    }
}
