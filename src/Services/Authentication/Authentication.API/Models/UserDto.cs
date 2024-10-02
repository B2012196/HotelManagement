namespace Authentication.API.Models
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int FailedLoginAttempt { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid RoleId { get; set; }
    }
}
