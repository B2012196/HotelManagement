namespace GuestManagement.API.Models
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
}
