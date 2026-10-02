using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laibrary_Payment_App.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");

            builder.HasKey(p => p.PaymentId);
            builder.Property(p => p.PaymentId)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.PaymentNo)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.InvoiceId)
                   .IsRequired();

            builder.Property(p => p.StudentId)
                   .IsRequired();

            builder.Property(p => p.PaymentDate)
                   .IsRequired();

            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PaymentMode)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasDefaultValue(PaymentModes.Cash);

            builder.Property(p => p.ReferenceNo)
                   .IsRequired(false)
                   .HasMaxLength(100);

            builder.Property(p => p.Remarks)
                   .IsRequired(false)
                   .HasMaxLength(500);

            builder.Property(p => p.ReceivedBy)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasDefaultValue("System");

            builder.Property(p => p.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.ModifiedDate)
                   .IsRequired(false);

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasMaxLength(30)
                   .HasDefaultValue("Active");

            builder.Property(p => p.CancelledBy)
                   .IsRequired(false)
                   .HasMaxLength(100);

            builder.Property(p => p.CancelledDate)
                   .IsRequired(false);

            // Indexes
            builder.HasIndex(p => p.PaymentNo)
                   .IsUnique()
                   .HasDatabaseName("IX_Payment_PaymentNo");

            builder.HasIndex(p => p.InvoiceId)
                   .HasDatabaseName("IX_Payment_InvoiceId");

            builder.HasIndex(p => p.StudentId)
                   .HasDatabaseName("IX_Payment_StudentId");

            builder.HasIndex(p => p.PaymentDate)
                   .HasDatabaseName("IX_Payment_PaymentDate");

            builder.HasIndex(p => p.PaymentMode)
                   .HasDatabaseName("IX_Payment_PaymentMode");

            builder.HasIndex(p => p.Status)
                   .HasDatabaseName("IX_Payment_Status");

            // Relationships
            builder.HasOne(p => p.Invoice)
                   .WithMany(i => i.Payments)
                   .HasForeignKey(p => p.InvoiceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Student)
                   .WithMany(s => s.Payments)
                   .HasForeignKey(p => p.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
