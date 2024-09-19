namespace IdentityManagement.API.Data.Configurations
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            // Đặt khóa chính
            builder.HasKey(t => t.TokenId);

            builder.Property(t => t.UserId)
                   .IsRequired();

            builder.Property(t => t.TokenContent)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(t => t.CreatedDate)
                   .IsRequired();

            builder.Property(t => t.ExpDate)
                   .IsRequired();

            builder.Property(t => t.Revoked)
                   .IsRequired()
                   .HasDefaultValue(false);

            // Tạo quan hệ với bảng User
            builder.HasOne(t => t.User)
                   .WithMany(u => u.Tokens)
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Đặt index cho TokenContent để tối ưu hóa tìm kiếm
            builder.HasIndex(t => t.TokenContent)
                   .IsUnique();
        }
    }
}
