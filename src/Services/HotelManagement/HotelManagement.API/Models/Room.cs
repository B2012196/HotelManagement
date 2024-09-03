namespace HotelManagement.API.Models
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string Number { get; set; }
        
        //Foreign key
        public Guid HotelId { get; set; }
        public Guid TypeId { get; set; }
        public Guid StatusId { get; set; }

        //Navigation Properties
        public Hotel Hotel { get; set; }
        public RoomType RoomType { get; set; }
        public RoomStatus RoomStatus { get; set; }
    }
}
