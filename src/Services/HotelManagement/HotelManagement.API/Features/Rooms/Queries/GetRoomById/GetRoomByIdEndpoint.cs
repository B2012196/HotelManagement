namespace HotelManagement.API.Features.Rooms.Queries.GetRoomById
{
    public record GetRoomByIdResponse(Room Room);
    public class GetRoomByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/rooms/roomid/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetRoomByIdQuery(id));

                var response = result.Adapt<GetRoomByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRoomsById")
            .Produces<GetRoomByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Rooms By Id")
            .WithDescription("Get Rooms By Id");
        }
    }
}
