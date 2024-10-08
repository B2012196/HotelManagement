namespace Admin.Web.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public record LoginResponse(Token Token, bool IsSuccess);
}
