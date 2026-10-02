using Laibrary_Payment_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laibrary_Payment_App.Data.Configurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("InvoiceDetail");

            builder.HasKey(d => d.InvoiceDetailId);
            builder.Property(d => d.InvoiceDetailId)
                   .ValueGeneratedOnAdd();

            builder.Property(d => d.InvoiceId)
                   .IsRequired();

            builder.Property(d => d.Description)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(d => d.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.Property(d => d.Rate)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(d => d.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasIndex(d => d.InvoiceId)
                   .HasDatabaseName("IX_InvoiceDetail_InvoiceId");

            builder.HasOne(d => d.Invoice)
                   .WithMany(i => i.InvoiceDetails)
                   .HasForeignKey(d => d.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
