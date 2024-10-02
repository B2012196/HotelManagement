﻿namespace Hotel.Web.Services
{
    public interface IAuthentication
    {
        [Post("/authentication/login")]
        Task<LoginResponse> Login(LoginModel loginModel);

        [Post("/authentication/users")]
        Task<CreateUserResponse> CreateUser(UserModel userModel);

    }
}