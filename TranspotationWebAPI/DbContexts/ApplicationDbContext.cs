using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TranspotationAPI.Model;
using TranspotationWebAPI.Model;

namespace TranspotationAPI.DbContexts
{
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
        public DbSet<Account> Accounts { get; set; }       
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<SitDetail> SitDetails { get; set; }
        public DbSet<TranCompany> TranCompanys { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}
