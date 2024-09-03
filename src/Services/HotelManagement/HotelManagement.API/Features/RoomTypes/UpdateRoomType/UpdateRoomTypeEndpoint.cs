
using HotelManagement.API.Features.Hotels.UpdateHotel;

namespace HotelManagement.API.Features.RoomTypes.UpdateRoomType
{
    public record UpdateRoomTypeRequest
        (Guid TypeId, string Name, string Description, decimal PricePerNight, int Capacity);
    public record UpdateRoomTypeResponse(bool IsSuccess);
    public class UpdateRoomTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/roomtypes", async (UpdateRoomTypeRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateRoomTypeCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateRoomTypeResponse>();  

                return Results.Ok(response);
            })
            .WithName("UpdateRoomType")
            .Produces<UpdateRoomTypeResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update RoomType")
            .WithDescription("Update RoomType");
        }
    }
}
