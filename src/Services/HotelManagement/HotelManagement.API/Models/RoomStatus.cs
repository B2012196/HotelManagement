using System.Text.Json.Serialization;

namespace HotelManagement.API.Models
{
    public class RoomStatus
    {
        public Guid StatusId { get; set; }
        public string Name { get; set; }

        //Navigation Property
        [JsonIgnore]
        public ICollection<Room> Rooms { get; set; }
    }
}
