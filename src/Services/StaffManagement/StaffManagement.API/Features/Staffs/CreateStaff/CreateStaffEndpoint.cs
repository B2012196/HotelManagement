namespace StaffManagement.API.Features.Staffs.CreateStaff
{
    public record CreateStaffRequest
        (Guid UserId, Guid HotelId, string FirstName, string LastName, decimal Salary, DateOnly DateofBirst,
        string Address, DateOnly HireDate);
    public record CreateStaffResponse(Guid StaffId);
    public class CreateStaffEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/staffs/staffs", async (CreateStaffRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateStaffCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateStaffResponse>();

                return Results.Created($"/staffs/staffs/{response.StaffId}", response);
            })
            .WithName("CreateStaff")
            .Produces<CreateStaffResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Staff")
            .WithDescription("Create Staff");
        }
    }
}
