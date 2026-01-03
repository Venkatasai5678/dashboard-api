using Microsoft.EntityFrameworkCore;
using backend.MODEL;
using ModelEntity.MODEL;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Do NOT create City table — you already have it manually
        public DbSet<City> Cities { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<CustomerEdit> CustomerEdits { get; set; }


        // Will create this new table
        public DbSet<Customer> CustomerDetails { get; set; }

        public DbSet<Contact> contacts { get; set; }
        public DbSet<ChartDataDto> ChartDataDto { get; set; }
        public DbSet<EmployeeName> EmployeeName { get; set; }
        public DbSet<EmployeeName> EmployeeName11 { get; set; }
       

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // 🧩 This tells EF that City already exists and should NOT be recreated
        //    modelBuilder.Entity<City>().ToTable("City").Metadata.SetIsTableExcludedFromMigrations(true);

        //    // ✅ CustomerDetailsSave already exists — don’t recreate
        //    modelBuilder.Entity<Customer>().ToTable("CustomerDetailsSave").Metadata.SetIsTableExcludedFromMigrations(true);

        //    modelBuilder.Entity<Contact>().ToTable("Contact");
        //}
    }
}