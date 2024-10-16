namespace Hotel.Web.Models
{
    public class Staff
    {
        public Guid StaffId { get; set; }
        public Guid UserId { get; set; }
        public Guid HotelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateofBirst { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public DateOnly HireDate { get; set; }
    }
    public record GetStaffsResponse(IEnumerable<Staff> Staffs);
    public record CreateStaffResponse(Guid StaffId);
    public record UpdateStaffResponse(bool IsSuccess);
    public record DeleteStaffResponse(bool IsSuccess);
}
