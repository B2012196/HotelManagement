namespace Hotel.Web.Services
{
    public interface IAuthentication
    {
        [Post("/authentication/login")]
        Task<LoginResponse> Login(Login login);

        [Post("/authentication/users")]
        Task<CreateUserResponse> CreateUser(User user);

        [Put("/authentication/users/password")]
        Task<UpdatePasswordResponse> UpdatePassword(object obj);

        [Get("/authentication/users/userid/{userid}")]
        Task<GetUserByUserIdResponse> GetUserByUserId(Guid UserId);

    }
}
