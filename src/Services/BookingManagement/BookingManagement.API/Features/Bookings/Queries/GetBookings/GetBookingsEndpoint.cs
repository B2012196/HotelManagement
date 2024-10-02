using BookingManagement.API.Features.Bookings.Queries.GetBookings;

namespace BookingManagement.API.Features.Bookings.Queries.GetBookings
{
    public record GetBookingsResponse(IEnumerable<Booking> Bookings);
    public class GetBookingsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookings", async (ISender sender) =>
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
