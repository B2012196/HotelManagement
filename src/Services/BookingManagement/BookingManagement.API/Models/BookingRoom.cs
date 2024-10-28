namespace BookingManagement.API.Models
{
    public class BookingRoom
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public Booking Booking { get; set; }
    }
}
