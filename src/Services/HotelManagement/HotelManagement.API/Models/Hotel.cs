using System.Text.Json.Serialization;

namespace HotelManagement.API.Models
{
    public class Hotel
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Stars { get; set; }
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public ICollection<Room> Rooms { get; set; }

    }
}
