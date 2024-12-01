namespace HotelManagement.API.Features.Rooms.Queries.GetRooms
{
    public record GetRoomsRequest(int? pageNumber = 1, int? pageSize = 10);
    public record GetRoomsResponse(IEnumerable<Room> Rooms, int TotalCount);
    public class GetRoomsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/rooms", async ([AsParameters] GetRoomsRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetRoomsQuery(request.pageNumber, request.pageSize));

                var response = result.Adapt<GetRoomsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRooms")
            .Produces<GetRoomsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Rooms")
            .WithDescription("Get Rooms");
        }
    }
}
