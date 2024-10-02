namespace Hotel.Web.Models
{
    public class TokenModel
    {
        public string Access_token { get; set; }         // JWT Token
        public int Expires_in { get; set; }              // Thời gian hết hạn của token (tính theo phút)

    }
}
