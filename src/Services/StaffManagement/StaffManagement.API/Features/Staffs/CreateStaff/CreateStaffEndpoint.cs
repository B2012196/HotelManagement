namespace StaffManagement.API.Features.Staffs.CreateStaff
{
    public record CreateStaffRequest
        (Guid HotelId, Guid StaffRoleId, string FirstName, string LastName, DateOnly DateofBirst,
        string Phone, string Address, string Email);
    public record CreateStaffResponse(Guid StaffId);
    public class CreateStaffEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/staffs", async (CreateStaffRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateStaffCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateStaffResponse>();

                return Results.Created($"/staffs/{response.StaffId}", response);
            })
            .WithName("CreateStaff")
            .Produces<CreateStaffResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Staff")
            .WithDescription("Create Staff");
        }
    }
}
