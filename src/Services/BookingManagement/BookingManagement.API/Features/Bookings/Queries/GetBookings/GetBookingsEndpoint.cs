namespace BookingManagement.API.Features.Bookings.Queries.GetBookings
{
    public record GetBookingsRequest(int? pageNumber = 1, int? pageSize = 10, string? filterStatus = null);
    public record GetBookingsResponse(IEnumerable<Booking> Bookings, int TotalCount);
    public class GetBookingsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookings", async ([AsParameters] GetBookingsRequest request, ISender sender) =>
            {
                BookingStatus? status = null;
                if (!string.IsNullOrEmpty(request.filterStatus))
                {
                    if (Enum.TryParse<BookingStatus>(request.filterStatus, true, out var parsedStatus))
                    {
                        status = parsedStatus;
                    }
                    else
                    {
                        return Results.BadRequest("Invalid filterStatus value.");
                    }
                }

                var result = await sender.Send(new GetBookingsQuery(request.pageNumber, request.pageSize, status));

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
