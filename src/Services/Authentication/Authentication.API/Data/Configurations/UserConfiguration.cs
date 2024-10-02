namespace Authentication.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Đặt khóa chính
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.RoleId)
                   .IsRequired();

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(15);

            builder.Property(u => u.FailedLoginAttempt)
                   .HasDefaultValue(0);

            builder.Property(u => u.IsActive)
                   .HasDefaultValue(true);

            builder.Property(u => u.CreateAt).HasColumnType("timestamp without time zone")
                   .IsRequired();

            // Tạo quan hệ với bảng User
            builder.HasOne(u => u.Role)
                   .WithMany(r => r.Users)
                   .HasForeignKey(u => u.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Đặt index cho Email để đảm bảo email duy nhất
            builder.HasIndex(u => u.Email)
                   .IsUnique();

            // Đặt index cho UserName để tối ưu hóa việc tìm kiếm
            builder.HasIndex(u => u.UserName)
                   .IsUnique();
        }
    }
}
