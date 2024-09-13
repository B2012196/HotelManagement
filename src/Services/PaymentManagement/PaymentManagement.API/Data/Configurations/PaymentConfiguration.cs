namespace PaymentManagement.API.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Định nghĩa khóa chính
            builder.HasKey(p => p.PaymentId);

            //Thiết lập kiểu dữ liệu cho thuộc tính BookingId
            builder.Property(p => p.BookingId).IsRequired();

            // Thiết lập kiểu dữ liệu cho thuộc tính Amount
            builder.Property(p => p.Amount)
                   .HasColumnType("decimal(12,2)")
                   .IsRequired();  // Bắt buộc phải có

            // Thiết lập ngày thanh toán
            builder.Property(p => p.PaymentDate).HasColumnType("timestamp without time zone");

            builder.Property(p => p.PaymentMethodId);

            // Payment - PaymentMethod
            builder.HasOne(p => p.PaymentMethod)
                   .WithMany(pm => pm.Payments)  // PaymentMethod có thể có nhiều Payment
                   .HasForeignKey(p => p.PaymentMethodId)
                   .OnDelete(DeleteBehavior.Restrict);  // Đảm bảo không xóa PaymentMethod khi xóa Payment

            // Cấu hình trạng thái thanh toán
            builder.Property(p => p.Status).HasConversion<int>()
                   .IsRequired();  // Bắt buộc phải có
        }
    }
}
