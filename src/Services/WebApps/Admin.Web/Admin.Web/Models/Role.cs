namespace Admin.Web.Models
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public record GetRolesResponse(IEnumerable<Role> Roles);
    public record CreateRoleResponse(Guid RoleId);
    public record UpdateRoleResponse(bool IsSuccess);
    public record DeleteRoleResponse(bool IsSuccess);
}
