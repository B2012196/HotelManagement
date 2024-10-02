namespace Authentication.API.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int FailedLoginAttempt { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }

        [JsonIgnore]
        public ICollection<Token> Tokens { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }

    }
}
