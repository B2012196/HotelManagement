namespace StaffManagement.API.Features.Staffs.GetStaffs
{
    public record GetStaffsResponse(IEnumerable<Staff> Staffs);
    public class GetStaffsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/staffs/staffs", async (ISender sender) =>
            {
                var result = await sender.Send(new GetStaffsQuery());

                var response = result.Adapt<GetStaffsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetStaffs")
            .Produces<GetStaffsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Staffs")
            .WithDescription("Get Staffs");
        }
    }
}
