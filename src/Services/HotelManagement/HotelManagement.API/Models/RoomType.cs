using System.Text.Json.Serialization;

namespace HotelManagement.API.Models
{
    public class RoomType
    {
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string ImageContentType { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }

        //Navigation Property
        [JsonIgnore]
        public ICollection<Room> Rooms { get; set; }
    }
}
