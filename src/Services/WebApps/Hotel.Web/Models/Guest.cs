namespace Hotel.Web.Models
{
    public class Guest
    {
        public Guid GuestId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirst { get; set; }
        public string Address { get; set; }
    }

    public record GetGuestsResponse(IEnumerable<Guest> Guests);
    public record GetGuestByIdResponse(Guest Guest);
    public record GetGuestByUserIdReponse(Guest Guest);
    public record CreateGuestReponse(Guid GuestId);
    public record UpdateGuestResponse(bool IsSuccess);
    public record DeleteGuestResponse(bool IsSuccess);
}
