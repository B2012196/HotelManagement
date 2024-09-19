namespace IdentityManagement.API.Models
{
    public class Token
    {
        public Guid TokenId { get; set; }
        public Guid UserId { get; set; }
        public string TokenContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpDate { get; set; }
        public bool Revoked { get; set; }
        public User User { get; set; }
    }
}
