namespace BookingManagement.API.Features.Bookings.UpdateBookingCheckin
{
    public record UpdateBookingCheckinRequest(Guid BookingId, DateTime CheckinDate);
    public record UpdateBookingCheckinResponse(bool IsSuccess);
    public class UpdateBookingCheckinEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/bookings/checkin", async(UpdateBookingCheckinRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateBookingCheckinCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateBookingCheckinResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateBookingCheckin")
            .Produces<UpdateBookingCheckinResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Booking")
            .WithDescription("Update Booking");
        }
    }
}
