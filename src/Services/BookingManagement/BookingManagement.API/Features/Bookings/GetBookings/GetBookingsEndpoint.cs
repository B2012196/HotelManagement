namespace BookingManagement.API.Features.Bookings.GetBookings
{
    public record GetBookingsResponse(IEnumerable<Booking> Bookings);
    public class GetBookingsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings", async (ISender sender) =>
            {
                var result = await sender.Send(new GetBookingsQuery());

                var response = result.Adapt<GetBookingsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBookings")
            .Produces<GetBookingsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Bookings")
            .WithDescription("Get Bookings");
        }
    }
}
