namespace BookingManagement.API.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public string BookingCode { get; set; }
        public Guid GuestId { get; set; }
        public Guid TypeId { get; set; }
        public DateTime ExpectedCheckinDate { get; set; }
        public DateTime ExpectedCheckoutDate { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int RoomQuantity { get; set; }
        public BookingStatus BookingStatus { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public ICollection<BookingRoom> BookingRooms { get; set; }
    }
}
