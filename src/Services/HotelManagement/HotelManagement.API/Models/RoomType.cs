namespace HotelManagement.API.Models
{
    public class RoomType
    {
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }

        // Navigation Property for Images
        [JsonIgnore]
        public ICollection<Image> Images { get; set; }

        // Navigation Property for Rooms
        [JsonIgnore]
        public ICollection<Room> Rooms { get; set; }
    }
}
