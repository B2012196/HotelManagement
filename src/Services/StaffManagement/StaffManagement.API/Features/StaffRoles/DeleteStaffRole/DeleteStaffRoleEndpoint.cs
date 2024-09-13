namespace StaffManagement.API.Features.StaffRoles.DeleteStaffRole
{
    public record DeleteStaffRoleResponse(bool IsSuccess);
    public class DeleteStaffRoleEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/staffroles/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteStaffRoleCommand(id));

                var response = result.Adapt<DeleteStaffRoleResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteStaffRole")
            .Produces<DeleteStaffRoleResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete StaffRole")
            .WithDescription("Delete StaffRole");
        }
    }
}
