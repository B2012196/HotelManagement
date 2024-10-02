namespace HotelManagement.API.Features.RoomStatuses.DeleteRoomStatus
{
    public record DeleteRoomStatusResponse(bool IsSuccess);
    public class DeleteRoomStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/hotels/roomstatus/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteRoomStatusCommand(id));

                var response = result.Adapt<DeleteRoomStatusResponse>();    

                return Results.Ok(response);

            })
            .WithName("DeleteRoomStatus")
            .Produces<DeleteRoomStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete RoomStatus")
            .WithDescription("Delete RoomStatus");
        }
    }
}
