namespace BookingManagement.API.Models
{
    public class RoomTypeResponseDTO
    {
        public RoomTypeDTO RoomType { get; set; }
    }
    public class RoomTypeDTO
    {
        public Guid typeId { get; set; }
        public string name { get; set; }
        public decimal pricePerNight { get; set; }
    }
}
