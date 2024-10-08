namespace HotelManagement.API.Features.Rooms.Queries.GetRoomsAvaByType
{
    public record GetRoomsAvaResponse(IEnumerable<Room> Rooms);
    public class GetRoomsAvaByTypeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/rooms/ava/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetRoomsAvaQuery(id));

                var response = result.Adapt<GetRoomsAvaResponse>(); 

                return Results.Ok(response);
            })
            .WithName("GetRoomsAvaByTypeId")
            .Produces<GetRoomsAvaResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Rooms Ava By TypeId")
            .WithDescription("Get Rooms Ava By TypeId");
        }
    }
}
