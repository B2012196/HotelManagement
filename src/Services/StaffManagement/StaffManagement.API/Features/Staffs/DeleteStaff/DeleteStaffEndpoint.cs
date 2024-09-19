namespace StaffManagement.API.Features.Staffs.DeleteStaff
{
    public record DeleteStaffResponse(bool IsSuccess);
    public class DeleteStaffEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/staffs/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteStaffCommand(id));

                var response = result.Adapt<DeleteStaffResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteStaff")
            .Produces<DeleteStaffResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Staff")
            .WithDescription("Delete Staff");
        }
    }
}
