namespace BookingManagement.API.Features.Bookings.CreateBooking
{
    public record CreateBookingRequest
        (Guid GuestId, DateTime ExpectedCheckinDate, DateTime ExpectedCheckoutDate);
    public record CreateBookingResponse(Guid BookingId);
    public class CreateBookingEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/bookings", async(CreateBookingRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateBookingCommand>();  

                var result = await sender.Send(command);

                var response = result.Adapt<CreateBookingResponse>();

                return Results.Created($"/bookings/{response.BookingId}", response);
            })
            .WithName("CreateBooking")
            .Produces<CreateBookingResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Booking")
            .WithDescription("Create Booking");
        }
    }
}
