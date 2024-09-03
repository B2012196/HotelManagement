
using HotelManagement.API.Features.Hotels.DeleteHotel;

namespace HotelManagement.API.Features.RoomTypes.DeleteRoomType
{
    public record DeleteRoomTypeResponse(bool IsSuccess);
    public class DeleteRoomTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/roomtypes/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteRoomTypeCommand(id));
                var response = result.Adapt<DeleteRoomTypeResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteRoomType")
            .Produces<DeleteRoomTypeResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete RoomType")
            .WithDescription("Delete RoomType");
        }
    }
}
