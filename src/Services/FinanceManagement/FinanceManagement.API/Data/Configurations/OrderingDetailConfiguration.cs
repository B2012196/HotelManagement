namespace FinanceManagement.API.Data.Configurations
{
    public class OrderingDetailConfiguration : IEntityTypeConfiguration<OrderingDetail>
    {
        public void Configure(EntityTypeBuilder<OrderingDetail> builder)
        {
            builder.HasKey(o => o.OrderingId);

            builder.Property(o => o.ServiceId)
                   .IsRequired();

            builder.Property(o => o.Numberofservice)
                   .IsRequired();

            builder.Property(o => o.TotalPrice)
               .HasColumnType("decimal(12,2)");

            builder.HasOne(od => od.Ordering)
                   .WithMany(o => o.OrderingDetails)
                   .HasForeignKey(od => od.OrderingId);

            builder.HasOne(od => od.Service)
                   .WithMany(s => s.OrderingDetails)
                   .HasForeignKey(od => od.ServiceId);
        }
    }
}
