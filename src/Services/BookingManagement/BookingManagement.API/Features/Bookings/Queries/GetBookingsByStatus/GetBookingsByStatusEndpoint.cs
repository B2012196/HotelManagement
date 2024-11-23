namespace BookingManagement.API.Features.Bookings.Queries.GetBookingsByStatus
{
    public record GetBookingsByStatusResponse(IEnumerable<Booking> Bookings);
    public class GetBookingsByStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookings/status/{status}", async (BookingStatus status, ISender sender) =>
            {
                var result = await sender.Send(new GetBookingsByStatusQuery(status));

                var response = result.Adapt<GetBookingsByStatusResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBookingsByStatus")
            .Produces<GetBookingsByStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Bookings By Status")
            .WithDescription("Get Bookings By Status");
        }
    }
}
