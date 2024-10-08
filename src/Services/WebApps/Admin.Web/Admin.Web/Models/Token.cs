namespace Admin.Web.Models
{
    public class Token
    {
        public string Access_token { get; set; }         // JWT Token
        public int Expires_in { get; set; }              // Thời gian hết hạn của token (tính theo phút)
    }
}
