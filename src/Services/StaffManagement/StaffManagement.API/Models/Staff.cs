namespace StaffManagement.API.Models
{
    public class Staff
    {
        public Guid StaffId { get; set; }
        public Guid HotelId { get; set; }
        public Guid StaffRoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateofBirst { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        //Navigation properties
        [JsonIgnore]
        public StaffRole StaffRole { get; set; }
    }
}
