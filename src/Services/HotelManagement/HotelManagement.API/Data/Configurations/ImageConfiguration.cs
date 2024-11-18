namespace HotelManagement.API.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.ImageId);

            builder.Property(t => t.Data).HasColumnType("bytea").IsRequired(false);  // Lưu trữ ảnh dưới dạng nhị phân lớn
            builder.Property(t => t.ContentType).IsRequired(false);

            //room - statusroom 
            builder.HasOne(i => i.RoomType)
                .WithMany(t => t.Images)
                .HasForeignKey(i => i.RoomTypeId);
        }
    }
}
