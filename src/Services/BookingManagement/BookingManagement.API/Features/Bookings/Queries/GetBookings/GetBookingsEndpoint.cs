namespace BookingManagement.API.Features.Bookings.Queries.GetBookings
{
    public record GetBookingsRequest(int? pageNumber = 1, int? pageSize = 10);
    public record GetBookingsResponse(IEnumerable<Booking> Bookings, int totalCount);
    public class GetBookingsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookings", async ([AsParameters] GetBookingsRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetBookingsQuery(request.pageNumber, request.pageSize));

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
