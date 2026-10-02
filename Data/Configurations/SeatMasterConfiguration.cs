using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laibrary_Payment_App.Data.Configurations
{
    public class SeatMasterConfiguration : IEntityTypeConfiguration<SeatMaster>
    {
        public void Configure(EntityTypeBuilder<SeatMaster> builder)
        {
            builder.ToTable("SeatMaster");

            builder.HasKey(s => s.SeatId);
            builder.Property(s => s.SeatId)
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.SeatNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(s => s.Area)
                   .HasMaxLength(50);

            builder.Property(s => s.Status)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasDefaultValue("Available");

            builder.Property(s => s.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(s => s.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.ModifiedDate)
                   .IsRequired(false);

            // Indexes
            builder.HasIndex(s => s.SeatNumber)
                   .IsUnique()
                   .HasDatabaseName("IX_SeatMaster_SeatNumber");

            // Development / Initial Seed Data
            builder.HasData(
                new SeatMaster
                {
                    SeatId = 1,
                    SeatNumber = "A01",
                    Area = "Main Study Hall",
                    Status = "Available",
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SeatMaster
                {
                    SeatId = 2,
                    SeatNumber = "A02",
                    Area = "Main Study Hall",
                    Status = "Available",
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SeatMaster
                {
                    SeatId = 3,
                    SeatNumber = "A03",
                    Area = "Main Study Hall",
                    Status = "Available",
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SeatMaster
                {
                    SeatId = 4,
                    SeatNumber = "A04",
                    Area = "Main Study Hall",
                    Status = "Available",
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new SeatMaster
                {
                    SeatId = 5,
                    SeatNumber = "A05",
                    Area = "Main Study Hall",
                    Status = "Available",
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
