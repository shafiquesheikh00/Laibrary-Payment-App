using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laibrary_Payment_App.Data.Configurations
{
    public class StudentMasterConfiguration : IEntityTypeConfiguration<StudentMaster>
    {
        public void Configure(EntityTypeBuilder<StudentMaster> builder)
        {
            builder.ToTable("StudentMaster");

            builder.HasKey(s => s.StudentId);
            builder.Property(s => s.StudentId)
                   .ValueGeneratedOnAdd();

            builder.Property(s => s.StudentCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(s => s.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(s => s.MobileNo)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(s => s.Email)
                   .HasMaxLength(150);

            builder.Property(s => s.Address)
                   .HasMaxLength(500);

            builder.Property(s => s.CollegeName)
                   .HasMaxLength(200);

            builder.Property(s => s.CourseName)
                   .HasMaxLength(100);

            builder.Property(s => s.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.Property(s => s.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(s => s.ModifiedDate)
                   .IsRequired(false);

            // Indexes
            builder.HasIndex(s => s.StudentCode)
                   .IsUnique()
                   .HasDatabaseName("IX_StudentMaster_StudentCode");

            builder.HasIndex(s => s.MobileNo)
                   .HasDatabaseName("IX_StudentMaster_MobileNo");
        }
    }
}
