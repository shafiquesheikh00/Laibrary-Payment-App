using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Laibrary_Payment_App.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StudentMaster> Students { get; set; } = null!;
        public virtual DbSet<MembershipPlan> MembershipPlans { get; set; } = null!;
        public virtual DbSet<SeatMaster> Seats { get; set; } = null!;
        public virtual DbSet<StudentMembership> StudentMemberships { get; set; } = null!;
        public virtual DbSet<UserMaster> Users { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = GetDefaultConnectionString();
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
        }

        public static string GetDefaultConnectionString()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var projectDir = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.Exists(basePath) ? basePath : projectDir)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            return configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
    }
}
