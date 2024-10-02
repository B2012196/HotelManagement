namespace Authentication.API.Features.Roles.CreateRole
{
    public record CreateRoleRequest(string RoleName);
    public record CreateRoleResponse(Guid RoleId);
    public class CreateRoleEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/authentication/roles", async (CreateRoleRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateRoleCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateRoleResponse>();

                return Results.Created($"/roles/{response.RoleId}", response);
            })
            .WithName("CreateRole")
            .Produces<CreateRoleResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Role")
            .WithDescription("Create Role");
        }
    }
}
