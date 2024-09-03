
using HotelManagement.API.Features.Hotels.GetHotels;

namespace HotelManagement.API.Features.RoomTypes.GetRoomTypes
{
    public record GetRoomTypeResponse(IEnumerable<RoomType> RoomTypes);
    public class GetRoomTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/roomtypes", async (ISender sender) =>
            {
                var result = await sender.Send(new GetRoomTypeQuery());

                var response = result.Adapt<GetRoomTypeResponse>();    

                return Results.Ok(response);
            })
            .WithName("GetRoomTypes")
            .Produces<GetRoomTypeResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get RoomTypes")
            .WithDescription("Get RoomTypes");
        }
    }
}
