using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.API.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.RoomId);
            builder.Property(r => r.Number).IsRequired();

            //room - statusroom 
            builder.HasOne(r => r.RoomStatus)
                .WithMany(rs => rs.Rooms)
                .HasForeignKey(r => r.StatusId);

            //room - typeroom
            builder.HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.TypeId);

            //room - hotel
            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId);
        }
    }
}
