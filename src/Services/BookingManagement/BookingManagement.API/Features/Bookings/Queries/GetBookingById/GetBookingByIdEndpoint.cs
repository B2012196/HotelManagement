namespace BookingManagement.API.Features.Bookings.Queries.GetBookingById
{
    public record GetBookingByIdResponse(Booking Booking);
    public class GetBookingByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookings/id/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetBookingByIdQuery(id));

                var response = result.Adapt<GetBookingByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBookingById")
            .Produces<GetBookingByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Booking By Id")
            .WithDescription("Get Booking By Id");
        }
    }
}
