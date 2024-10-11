namespace HotelManagement.API.Features.RoomStatuses.CreateRoomStatus
{
    public record CreateRoomStatusRequest(string Name);
    public record CreateRoomStatusResponse(Guid StatusId);
    public class CreateRoomStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/hotels/roomstatus", async (CreateRoomStatusRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateRoomStatusCommand>();

                var result = await sender.Send(command);    

                var response = result.Adapt<CreateRoomStatusResponse>();

                return Results.Created($"/hotels/roomstatus/{response.StatusId}", response);
            })
            .WithName("CreateRoomStatus")
            .Produces<CreateRoomStatusResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create RoomStatus")
            .WithDescription("Create RoomStatus");
        }
    }
}
