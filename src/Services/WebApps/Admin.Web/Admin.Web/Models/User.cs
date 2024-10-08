namespace Admin.Web.Models
{
    public class User
    {
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }
    public record GetUsersResponse(IEnumerable<UserDto> UserDtos);
    public record CreateUserResponse(Guid UserId);
    public record UpdateUserResponse(bool IsSuccess);
    public record DeleteUserResponse(bool IsSuccess);
}
