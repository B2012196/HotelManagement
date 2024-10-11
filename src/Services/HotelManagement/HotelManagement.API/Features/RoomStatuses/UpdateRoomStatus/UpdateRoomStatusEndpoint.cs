namespace HotelManagement.API.Features.RoomStatuses.UpdateRoomStatus
{
    public record UpdateRoomStatusRequest(Guid StatusId, string Name);
    public record UpdateRoomStatusResponse(bool IsSuccess);
    public class UpdateRoomStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/hotels/roomstatus", async (UpdateRoomStatusRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateRoomStatusCommand>();

                var result = await sender.Send(command);    

                var response = result.Adapt<UpdateRoomStatusResponse>();   

                return Results.Ok(response);
            })
            .WithName("UpdateRoomStatus")
            .Produces<UpdateRoomStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update RoomStatus")
            .WithDescription("Update RoomStatus");
        }
    }
}
