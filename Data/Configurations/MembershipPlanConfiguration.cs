using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laibrary_Payment_App.Data.Configurations
{
    public class MembershipPlanConfiguration : IEntityTypeConfiguration<MembershipPlan>
    {
        public void Configure(EntityTypeBuilder<MembershipPlan> builder)
        {
            builder.ToTable("MembershipPlan");

            builder.HasKey(p => p.PlanId);
            builder.Property(p => p.PlanId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.PlanName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.DurationValue)
                   .IsRequired();

            builder.Property(p => p.DurationUnit)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.Fee)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(p => p.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.ModifiedDate)
                   .IsRequired(false);

            builder.HasIndex(p => p.PlanName)
                   .HasDatabaseName("IX_MembershipPlan_PlanName");

            // Development / Initial Seed Data
            builder.HasData(
                new MembershipPlan
                {
                    PlanId = 1,
                    PlanName = "Daily",
                    DurationValue = 1,
                    DurationUnit = "Day",
                    Fee = 50.00m,
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new MembershipPlan
                {
                    PlanId = 2,
                    PlanName = "Weekly",
                    DurationValue = 1,
                    DurationUnit = "Week",
                    Fee = 250.00m,
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new MembershipPlan
                {
                    PlanId = 3,
                    PlanName = "Monthly",
                    DurationValue = 1,
                    DurationUnit = "Month",
                    Fee = 800.00m,
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new MembershipPlan
                {
                    PlanId = 4,
                    PlanName = "Quarterly",
                    DurationValue = 3,
                    DurationUnit = "Month",
                    Fee = 2200.00m,
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new MembershipPlan
                {
                    PlanId = 5,
                    PlanName = "Yearly",
                    DurationValue = 1,
                    DurationUnit = "Year",
                    Fee = 8000.00m,
                    IsActive = true,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
