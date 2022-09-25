using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TranspotationWebAPI.Model;

namespace TranspotationAPI.DbContexts
{
    // For create database and table for the Model
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //try
            //{
            //    var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //    if (databaseCreator != null)
            //    {
            //        if (!databaseCreator.CanConnect()) databaseCreator.Create();
            //        if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            //    }
            //} catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasOne(l => l.From)
                .WithMany(l => l.FromTrip)
                .HasForeignKey(l => l.From_Id)
                .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<Trip>()
                .HasOne(l => l.To)
                .WithMany(l => l.ToTrip)
                .HasForeignKey(l => l.To_Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }

        //Create Table
        public DbSet<Account> Account { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<CarType> CarType { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyTrip> CompanyTrip { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Station> Station { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Trip> Trip { get; set; }


    }
}
