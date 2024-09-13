namespace PaymentManagement.API.Data.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            // Định nghĩa khóa chính
            builder.HasKey(pm => pm.PaymentMethodId);

            //Thiết lập kiểu dữ liệu cho thuộc tính BookingId
            builder.Property(pm => pm.PaymentMethodName).IsRequired();
        }
    }
}
