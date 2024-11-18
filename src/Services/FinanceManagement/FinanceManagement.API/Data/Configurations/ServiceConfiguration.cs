namespace FinanceManagement.API.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.ServiceId);

            builder.Property(s => s.ServiceName)
                   .IsRequired();

            builder.Property(o => o.ServicePrice);
            builder.Property(o => o.ServiceImage).HasColumnType("bytea").IsRequired(false);  // Lưu trữ ảnh dưới dạng nhị phân lớn
        }
    }
}
