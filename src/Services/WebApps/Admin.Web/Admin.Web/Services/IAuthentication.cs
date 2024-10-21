namespace Admin.Web.Services
{
    public interface IAuthentication
    {
        //user
        [Get("/authentication/users")]
        Task<GetUsersResponse> GetUsers();
        [Post("/authentication/login")]
        Task<LoginResponse> Login(Login loginModel);

        [Post("/authentication/users")]
        Task<CreateUserResponse> CreateUser(User userModel);

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
