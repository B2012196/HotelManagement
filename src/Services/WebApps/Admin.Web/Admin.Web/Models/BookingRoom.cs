namespace Admin.Web.Models
{
    public class BookingRoom
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
    }
    public record GetBookingRoomsResponse(IEnumerable<BookingRoom> BookingRooms);
    public record GetBookRoomsByIdResponse(IEnumerable<BookingRoom> BookingRooms);
}
