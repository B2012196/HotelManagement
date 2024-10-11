namespace Admin.Web.Models
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string Number { get; set; }
        public Guid HotelId { get; set; }
        public Guid TypeId { get; set; }
        public Guid StatusId { get; set; }
    }

    public class RoomView
    {
        public Guid RoomId { get; set; }
        public string Number { get; set; }
        public Guid TypeId { get; set; }
        public string TypeName { get; set; }
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public record GetRoomsResponse(IEnumerable<Room> Rooms);
    public record GetRoomsAvaResponse(IEnumerable<Room> Rooms);
    public record GetRoomsByTypeResponse(IEnumerable<Room> Rooms);
    public record CreateRoomResponse(Guid RoomId);
    public record UpdateRoomResponse(bool IsSuccess);
    public record DeleteRoomResponse(bool IsSuccess);
}
