namespace Hotel.Web.Models
{
    public class GuestModel
    {
        public Guid GuestId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateofBirst { get; set; }
        public string Address { get; set; }
    }
}
