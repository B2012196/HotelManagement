﻿namespace Admin.Web.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }

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

    public class UserView
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int FailedLoginAttempt { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public record GetUsersResponse(IEnumerable<UserDto> UserDtos);
    public record GetUserByPhoneResponse(UserDto UserDto);
    public record CreateUserResponse(Guid UserId);
    public record UpdateUserResponse(bool IsSuccess);
    public record DeleteUserResponse(bool IsSuccess);
}
