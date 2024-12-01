
namespace BookingManagement.API.Features.BookingRooms.GetBookRoomsByBookId
{
    public record GetBookRoomsByIdResponse(IEnumerable<BookingRoom> BookingRooms);
    public class GetBookRoomsByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookingrooms/id/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetBookRoomsByIdQuery(id));  

                var response = result.Adapt<GetBookRoomsByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBookingRoomsById")
            .Produces<GetBookRoomsByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get BookingRooms By Id")
            .WithDescription("Get BookingRooms By Id");
        }
    }
}
