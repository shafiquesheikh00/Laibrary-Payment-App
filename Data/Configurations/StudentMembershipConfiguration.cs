using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laibrary_Payment_App.Data.Configurations
{
    public class StudentMembershipConfiguration : IEntityTypeConfiguration<StudentMembership>
    {
        public void Configure(EntityTypeBuilder<StudentMembership> builder)
        {
            builder.ToTable("StudentMembership");

            builder.HasKey(m => m.MembershipId);
            builder.Property(m => m.MembershipId)
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.StudentId)
                   .IsRequired();

            builder.Property(m => m.PlanId)
                   .IsRequired();

            builder.Property(m => m.SeatId)
                   .IsRequired(false);

            builder.Property(m => m.StartDate)
                   .IsRequired();

            builder.Property(m => m.EndDate)
                   .IsRequired();

            builder.Property(m => m.ActualFee)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(m => m.DiscountAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(m => m.FinalFee)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(m => m.Status)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasDefaultValue("Active");

            builder.Property(m => m.Remarks)
                   .IsRequired(false)
                   .HasMaxLength(500);

            builder.Property(m => m.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(m => m.ModifiedDate)
                   .IsRequired(false);

            // Indexes
            builder.HasIndex(m => m.StartDate)
                   .HasDatabaseName("IX_StudentMembership_StartDate");

            builder.HasIndex(m => m.EndDate)
                   .HasDatabaseName("IX_StudentMembership_EndDate");

            builder.HasIndex(m => m.Status)
                   .HasDatabaseName("IX_StudentMembership_Status");

            builder.HasIndex(m => new { m.StudentId, m.Status })
                   .HasDatabaseName("IX_StudentMembership_Student_Status");

            builder.HasIndex(m => new { m.SeatId, m.Status })
                   .HasDatabaseName("IX_StudentMembership_Seat_Status");

            // Relationships
            builder.HasOne(m => m.Student)
                   .WithMany(s => s.StudentMemberships)
                   .HasForeignKey(m => m.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Plan)
                   .WithMany(p => p.StudentMemberships)
                   .HasForeignKey(m => m.PlanId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Seat)
                   .WithMany(s => s.StudentMemberships)
                   .HasForeignKey(m => m.SeatId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
