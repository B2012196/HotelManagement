using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.API.Data.Configurations
{
    public class RoomStatusConfiguration : IEntityTypeConfiguration<RoomStatus>
    {
        public void Configure(EntityTypeBuilder<RoomStatus> builder)
        {
            builder.HasKey(s => s.StatusId);
            builder.Property(h => h.Name).IsRequired().HasMaxLength(100);
        }
    }
}
