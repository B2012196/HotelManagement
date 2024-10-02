using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.API.Data.Configurations
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasKey(t => t.TypeId);
            builder.Property(h => h.Name).IsRequired().HasMaxLength(100);
            builder.Property(h => h.Description).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Image).HasColumnType("bytea").IsRequired(false);  // Lưu trữ ảnh dưới dạng nhị phân lớn
            builder.Property(t => t.ImageContentType).IsRequired(false);
            builder.Property(h => h.PricePerNight).IsRequired();
            builder.Property(h => h.Capacity).IsRequired();
        }
    }
}
