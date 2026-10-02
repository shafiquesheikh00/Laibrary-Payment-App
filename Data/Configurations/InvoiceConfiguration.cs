using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laibrary_Payment_App.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice");

            builder.HasKey(i => i.InvoiceId);
            builder.Property(i => i.InvoiceId)
                   .ValueGeneratedOnAdd();

            builder.Property(i => i.InvoiceNo)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(i => i.StudentId)
                   .IsRequired();

            builder.Property(i => i.MembershipId)
                   .IsRequired(false);

            builder.Property(i => i.InvoiceDate)
                   .IsRequired();

            builder.Property(i => i.DueDate)
                   .IsRequired();

            builder.Property(i => i.SubTotal)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.DiscountAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(i => i.AdditionalCharges)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(i => i.TaxAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(i => i.CurrentCharges)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.PreviousOutstanding)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(i => i.GrandTotal)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.PaidAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(i => i.BalanceAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Status)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasDefaultValue("Unpaid");

            builder.Property(i => i.CreatedBy)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasDefaultValue("System");

            builder.Property(i => i.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(i => i.ModifiedBy)
                   .IsRequired(false)
                   .HasMaxLength(100);

            builder.Property(i => i.ModifiedDate)
                   .IsRequired(false);

            // Indexes
            builder.HasIndex(i => i.InvoiceNo)
                   .IsUnique()
                   .HasDatabaseName("IX_Invoice_InvoiceNo");

            builder.HasIndex(i => i.StudentId)
                   .HasDatabaseName("IX_Invoice_StudentId");

            builder.HasIndex(i => i.MembershipId)
                   .HasDatabaseName("IX_Invoice_MembershipId");

            builder.HasIndex(i => i.InvoiceDate)
                   .HasDatabaseName("IX_Invoice_InvoiceDate");

            builder.HasIndex(i => i.Status)
                   .HasDatabaseName("IX_Invoice_Status");

            // Relationships
            builder.HasOne(i => i.Student)
                   .WithMany(s => s.Invoices)
                   .HasForeignKey(i => i.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Membership)
                   .WithMany(m => m.Invoices)
                   .HasForeignKey(i => i.MembershipId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.InvoiceDetails)
                   .WithOne(d => d.Invoice)
                   .HasForeignKey(d => d.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
