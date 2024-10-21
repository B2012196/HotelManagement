namespace Hotel.Web.Services
{
    public interface IAuthentication
    {
        [Post("/authentication/login")]
        Task<LoginResponse> Login(Login login);

        [Post("/authentication/users")]
        Task<CreateUserResponse> CreateUser(UserModel userModel);

        [Put("/authentication/users/password")]
        Task<UpdatePasswordResponse> UpdatePassword(object obj);

    }
}
