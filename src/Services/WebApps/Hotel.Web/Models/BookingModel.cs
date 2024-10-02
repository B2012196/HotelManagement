namespace Hotel.Web.Models
{
    public class BookingModel
    {
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
