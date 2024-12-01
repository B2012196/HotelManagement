
namespace HotelManagement.API.Features.Rooms.Queries.GetRoomsAva
{
    public record GetRoomsAvaResponse(IEnumerable<Room> Rooms);
    public class GetRoomsAvaEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/rooms/available", async (ISender sender) =>
            {
                var result = await sender.Send(new GetRoomsAvaQuery());

                var response = result.Adapt<GetRoomsAvaResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRoomsAvailable")
            .Produces<GetRoomsAvaResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Rooms Available")
            .WithDescription("Get Rooms Available");
        }
    }
}
