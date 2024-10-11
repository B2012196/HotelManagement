namespace StaffManagement.API.Features.Staffs.UpdateStaff
{
    public record UpdateStaffRequest(Guid StaffId, Guid UserId, Guid HotelId, string FirstName, string LastName, decimal Salary, DateOnly DateofBirst,
        string Address, DateOnly HireDate);
    public record UpdateStaffResponse(bool IsSuccess);
    public class UpdateStaffEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/staffs/staffs", async (UpdateStaffRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateStaffCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateStaffResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateStaff")
            .Produces<UpdateStaffResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Staff")
            .WithDescription("Update Staff");
        }
    }
}
