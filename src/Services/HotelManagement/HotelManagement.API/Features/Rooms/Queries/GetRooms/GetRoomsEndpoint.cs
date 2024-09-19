namespace HotelManagement.API.Features.Rooms.Queries.GetRooms
{
    public record GetRoomsResponse(IEnumerable<Room> Rooms);
    public class GetRoomsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/rooms", async (ISender sender) =>
            {
                var result = await sender.Send(new GetRoomsQuery());

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
