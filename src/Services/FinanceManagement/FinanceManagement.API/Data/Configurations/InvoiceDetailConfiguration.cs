namespace FinanceManagement.API.Data.Configurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasKey(i => i.DetailId);

            builder.Property(o => o.ServiceId)
                   .IsRequired();

            builder.Property(o => o.Numberofservice)
                   .IsRequired();

            builder.Property(o => o.TotalPrice)
               .HasColumnType("decimal(12,2)");

            builder.HasOne(od => od.Invoice)
                   .WithMany(o => o.InvoiceDetails)
                   .HasForeignKey(od => od.InvoiceId);

            builder.HasOne(od => od.Service)
                   .WithMany(s => s.InvoiceDetails)
                   .HasForeignKey(od => od.ServiceId);
        }
    }
}
