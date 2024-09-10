namespace BookingManagement.API.Features.Bookings.UpdateBookingCheckout
{
    public record UpdateBookingCheckoutRequest(Guid BookingId, DateTime CheckoutDate)
        : ICommand<UpdateBookingCheckoutResponse>;
    public record UpdateBookingCheckoutResponse(bool IsSuccess);
    public class UpdateBookingCheckoutEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/bookings/checkout", async (UpdateBookingCheckoutRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateBookingCheckoutCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateBookingCheckoutResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateBookingCheckout")
            .Produces<UpdateBookingCheckoutResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Booking")
            .WithDescription("Update Booking");
        }
    }
}
