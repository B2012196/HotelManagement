
using HotelManagement.API.Features.Hotels.DeleteHotel;

namespace HotelManagement.API.Features.Rooms.DeleteRoom
{
    public record DeleteRoomResponse(bool IsSuccess);
    public class DeleteRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/rooms/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteRoomCommand(id));

                var response = result.Adapt<DeleteRoomResponse>();  

                return Results.Ok(response);
            })
            .WithName("DeleteRoom")
            .Produces<DeleteRoomResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Room")
            .WithDescription("Delete Room");
        }
    }
}
