namespace BookingManagement.API.Features.Bookings.Commands.DeleteBooking
{
    public record DeleteBookingResponse(bool IsSuccess);
    public class DeleteBookingEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/bookings/bookings/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBookingCommand(id));

                var response = result.Adapt<DeleteBookingResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteBooking")
            .Produces<DeleteBookingResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Booking")
            .WithDescription("Delete Booking");
        }
    }
}
