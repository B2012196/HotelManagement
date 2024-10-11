namespace HotelManagement.API.Features.RoomStatuses.GetRoomStatusById
{
    public record GetRoomStatusByIdResponse(RoomStatus RoomStatus);
    public class GetRoomStatusByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/roomstatus/id/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetRoomStatusByIdQuery(id));

                var response = result.Adapt<GetRoomStatusByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRoomStatusById")
            .Produces<GetRoomStatusByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get RoomStatus By Id")
            .WithDescription("Get RoomStatus By Id");
        }
    }
}
