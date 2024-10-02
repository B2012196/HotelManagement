﻿namespace Hotel.Web.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public record LoginResponse(TokenModel Token, bool IsSuccess);
}
