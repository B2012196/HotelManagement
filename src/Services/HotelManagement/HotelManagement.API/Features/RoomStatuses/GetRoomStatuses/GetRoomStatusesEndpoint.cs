
using HotelManagement.API.Features.RoomTypes.GetRoomTypes;

namespace HotelManagement.API.Features.RoomStatuses.GetRoomStatuses
{
    public record GetRoomStatusesResponse(IEnumerable<RoomStatus> Statuses);
    public class GetRoomStatusesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/roomstatus", async (ISender sender) =>
            {
                var result = await sender.Send(new GetRoomStatusesQuery());

                var response = result.Adapt<GetRoomStatusesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRoomStatuses")
            .Produces<GetRoomStatusesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get RoomStatuses")
            .WithDescription("Get RoomStatuses");
        }
    }
}
