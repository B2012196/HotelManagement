namespace Admin.Web.Services
{
    public interface IAuthentication
    {
        [Get("/authentication/users")]
        Task<GetUsersResponse> GetUsers();
        [Post("/authentication/login")]
        Task<LoginResponse> Login(Login loginModel);

        [Post("/authentication/users")]
        Task<CreateUserResponse> CreateUser(User userModel);

    }
}
