namespace StaffManagement.API.Features.Staffs.UpdateStaff
{
    public record UpdateStaffRequest(Guid StaffId, Guid HotelId, Guid StaffRoleId, string FirstName, string LastName, DateOnly DateofBirst,
        string Phone, string Address, string Email);
    public record UpdateStaffResponse(bool IsSuccess);
    public class UpdateStaffEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/staffs", async (UpdateStaffRequest request, ISender sender) =>
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
