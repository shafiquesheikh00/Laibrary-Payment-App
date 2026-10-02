using System;
using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laibrary_Payment_App.Data.Configurations
{
    public class UserMasterConfiguration : IEntityTypeConfiguration<UserMaster>
    {
        public void Configure(EntityTypeBuilder<UserMaster> builder)
        {
            builder.ToTable("UserMaster");

            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .HasMaxLength(150);

            builder.Property(u => u.MobileNo)
                   .HasMaxLength(20);

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasDefaultValue("Admin");

            builder.Property(u => u.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(u => u.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.LastLoginDate)
                   .IsRequired(false);

            // Unique index on Username
            builder.HasIndex(u => u.Username)
                   .IsUnique()
                   .HasDatabaseName("IX_UserMaster_Username");

            // Seed initial Administrator user (username: admin / password: admin)
            builder.HasData(new UserMaster
            {
                UserId = 1,
                Username = "admin",
                // SHA-256 hash of "admin"
                PasswordHash = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918",
                FullName = "System Administrator",
                Email = "admin@library.com",
                MobileNo = "9999999999",
                Role = "Admin",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        }
    }
}
