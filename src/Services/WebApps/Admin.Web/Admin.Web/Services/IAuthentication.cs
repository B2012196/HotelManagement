namespace Admin.Web.Services
{
    public interface IAuthentication
    {
        //user
        [Get("/authentication/users")]
        Task<GetUsersResponse> GetUsers();

        [Get("/authentication/users/phone/{phone}")]
        Task<GetUserByPhoneResponse> GetUserByPhone(string phone);

        [Post("/authentication/login")]
        Task<LoginResponse> Login(Login loginModel);

        [Post("/authentication/users")]
        Task<CreateUserResponse> CreateUser(User userModel);

        [Delete("/authentication/users/{UserId}")]
        Task<DeleteUserResponse> DeleteUser(Guid UserId);

        //role
        [Get("/authentication/roles")]
        Task<GetRolesResponse> GetRoles();

        [Post("/authentication/roles")]
        Task<CreateRoleResponse> CreateRole(object obj);

        [Put("/authentication/roles")]
        Task<UpdateRoleResponse> UpdateRole(Role Role);

        [Delete("/authentication/roles/{RoleId}")]
        Task<DeleteRoleResponse> DeleteRole(Guid RoleId);


    }
}
