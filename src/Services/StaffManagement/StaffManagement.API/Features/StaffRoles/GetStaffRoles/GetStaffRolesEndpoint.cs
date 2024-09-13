namespace StaffManagement.API.Features.StaffRoles.GetStaffRoles
{
    public record GetStaffRolesResponse(IEnumerable<StaffRole> StaffRoles);
    public class GetStaffRolesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/staffroles", async (ISender sender) =>
            {
                var result = await sender.Send(new GetStaffRolesQuery());

                var response = result.Adapt<GetStaffRolesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetStaffRoles")
            .Produces<GetStaffRolesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get StaffRoles")
            .WithDescription("Get StaffRoles");
        }
    }
}
