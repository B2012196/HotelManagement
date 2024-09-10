namespace BookingManagement.API.Features.Bookings.UpdateBookingConfirm
{
    public record UpdateBookingConfirmRequest(Guid BookingId, Guid RoomId);
    public record UpdateBookingConfirmResponse(bool IsSuccess);
    public class UpdateBookingConfirmEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("bookings/confirm", async (UpdateBookingConfirmRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateBookingConfirmCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateBookingConfirmResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateBookingConfirm")
            .Produces<UpdateBookingConfirmResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Booking")
            .WithDescription("Update Booking");
        }
    }
}
