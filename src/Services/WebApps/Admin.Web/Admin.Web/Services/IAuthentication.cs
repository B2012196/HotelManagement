namespace Admin.Web.Services
{
    public interface IAuthentication
    {
        [Post("/authentication/login")]
        Task<LoginResponse> Login(Login loginModel);

        [Post("/authentication/users")]
        Task<CreateUserResponse> CreateUser(User userModel);

    }
}
