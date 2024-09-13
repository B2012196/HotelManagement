namespace StaffManagement.API.Features.StaffRoles.UpdateStaffRole
{
    public record UpdateStaffRoleRequest(Guid StaffRoleId, string StaffRoleName) : ICommand<UpdateStaffRoleResult>;
    public record UpdateStaffRoleResponse(bool IsSuccess);
    public class UpdateStaffRoleEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/staffroles", async (UpdateStaffRoleRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateStaffRoleCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateStaffRoleResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateStaffRole")
            .Produces<UpdateStaffRoleResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update StaffRole")
            .WithDescription("Update StaffRole");
        }
    }
}
