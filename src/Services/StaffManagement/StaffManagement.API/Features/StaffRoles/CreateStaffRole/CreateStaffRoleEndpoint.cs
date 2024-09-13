namespace StaffManagement.API.Features.StaffRoles.CreateStaffRole
{
    public record CreateStaffRoleRequest(string StaffRoleName);
    public record CreateStaffRoleResponse(Guid StaffRoleId);
    public class CreateStaffRoleEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/staffroles", async (CreateStaffRoleRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateStaffRoleCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateStaffRoleResponse>();

                return Results.Created($"/staffroles/{response.StaffRoleId}", response);
            })
            .WithName("CreateStaffRole")
            .Produces<CreateStaffRoleResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create StaffRole")
            .WithDescription("Create StaffRole");
        }
    }
}
