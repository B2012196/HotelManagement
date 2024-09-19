using HotelManagement.API.Features.Hotels.UpdateHotel;

namespace HotelManagement.API.Features.Rooms.Commands.UpdateRoom
{
    public record UpdateRoomRequest
        (Guid RoomId, string Number, Guid HotelId, Guid TypeId, Guid StatusId);
    public record UpdateRoomResponse(bool IsSuccess);
    public class UpdateRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/rooms", async (UpdateRoomRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateRoomCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateRoomResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateRoom")
            .Produces<UpdateRoomResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Room")
            .WithDescription("Update Room");
        }
    }
}
