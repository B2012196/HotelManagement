namespace BookingManagement.API.Features.BookingRooms.GetBookingRooms
{
    public record GetBookingRoomsResponse(IEnumerable<BookingRoom> BookingRooms);
    public class GetBRoomsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookingrooms", async (ISender sender) =>
            {
                var result = await sender.Send(new GetBookingRoomsQuery());

                var response = result.Adapt<GetBookingRoomsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBookingRooms")
            .Produces<GetBookingRoomsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get BookingRooms")
            .WithDescription("Get BookingRooms");
        }
    }
}
