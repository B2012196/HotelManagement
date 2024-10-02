namespace Hotel.Web.Models
{
    public class HotelModel
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Stars { get; set; }
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
    }

    //wrapper classes
    public record GetHotelsResponse(IEnumerable<HotelModel> Hotels);
    public record CreateHotelResponse(Guid Id);
    public record UpdateHotelResponse(bool IsSuccess);
    public record DeleteHotelResponse(bool IsSuccess);
}
