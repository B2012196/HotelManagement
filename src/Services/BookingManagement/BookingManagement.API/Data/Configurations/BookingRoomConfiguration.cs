
namespace BookingManagement.API.Data.Configurations
{
    public class BookingRoomConfiguration : IEntityTypeConfiguration<BookingRoom>
    {
        public void Configure(EntityTypeBuilder<BookingRoom> builder)
        {
            builder.HasKey(br => new { br.BookingId, br.RoomId }); // Thiet lap khoa chinh BookingId va RoomId

            builder.Property(br => br.BookingId)
                   .IsRequired();

            builder.Property(br => br.RoomId)
                   .IsRequired();

            // BookingRoom và Booking
            builder.HasOne(br => br.Booking)
                   .WithMany(b => b.BookingRooms) 
                   .HasForeignKey(br => br.BookingId);
        }
    }
}
