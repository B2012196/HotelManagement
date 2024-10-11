namespace StaffManagement.API.Data.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            // Thiết lập khóa chính
            builder.HasKey(s => s.StaffId);

            // Thiết lập bắt buộc cho các trường
            builder.Property(s => s.UserId).IsRequired();

            builder.Property(s => s.HotelId).IsRequired();

            builder.Property(s => s.FirstName).IsRequired().HasMaxLength(20);

            builder.Property(s => s.LastName).IsRequired().HasMaxLength(20);

            builder.Property(s => s.DateofBirst).IsRequired();

            builder.Property(s => s.Salary).IsRequired();

            builder.Property(s => s.Address).IsRequired().HasMaxLength(200);

            builder.Property(s => s.DateofBirst).IsRequired();

        }
    }
}
