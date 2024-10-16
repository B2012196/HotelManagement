namespace BookingManagement.API.Features.Bookings.Queries.GetBookingsByGuestId
{
    public record GetBookingByGuestIdResponse(IEnumerable<Booking> Bookings);
    public class GetBookingByGuestIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookings/guestid/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetBookingByGuestIdRQuery(id));

                var resposne = result.Adapt<GetBookingByGuestIdResponse>();

                return Results.Ok(resposne);
            })
            .WithName("GetBookingsByGuest")
            .Produces<GetBookingByGuestIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Bookings By Guest")
            .WithDescription("Get Bookings By Guest");
        }
    }
}
