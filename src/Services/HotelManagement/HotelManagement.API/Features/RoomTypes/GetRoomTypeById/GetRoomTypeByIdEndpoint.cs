namespace HotelManagement.API.Features.RoomTypes.GetRoomTypeById
{
    public record GetRoomTypeByIdResponse(RoomType RoomType);
    public class GetRoomTypeByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/roomtypes/id/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetRoomTypeByIdQuery(id));

                var response = result.Adapt<GetRoomTypeByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRoomTypeById")
            .Produces<GetRoomTypeByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get RoomType By Id")
            .WithDescription("Get RoomType By Id");
        }
    }
}
