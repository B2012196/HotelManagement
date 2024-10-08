namespace HotelManagement.API.Features.Rooms.Queries.GetRoomsByType
{
    public record GetRoomsByTypeResponse(IEnumerable<Room> Rooms);
    public class GetRoomsByTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/rooms/type/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetRoomsByTypeQuery(id));

                var response = result.Adapt<GetRoomsByTypeResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRoomsByTypeId")
            .Produces<GetRoomsByTypeResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Rooms By TypeId")
            .WithDescription("Get Rooms By TypeId");
        }
    }
}
