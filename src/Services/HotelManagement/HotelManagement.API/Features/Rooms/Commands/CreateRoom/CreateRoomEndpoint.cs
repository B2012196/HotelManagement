namespace HotelManagement.API.Features.Rooms.Commands.CreateRoom
{
    public record CreateRoomRequest(string Number, Guid HotelId, Guid TypeId, Guid StatusId);
    public record CreateRoomResponse(Guid RoomId);
    public class CreateRoomEndpoint() : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/hotels/rooms", async (CreateRoomRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateRoomCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateRoomResponse>();

                return Results.Created($"/romms/{response.RoomId}", response);
            })
            .WithName("CreateRoom")
            .Produces<CreateRoomResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Room")
            .WithDescription("Create Room");
        }
    }
}
