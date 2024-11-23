
namespace BookingManagement.API.Features.Bookings.Queries.GetBookingByCode
{
    public record GetBookingByCodeResponse(Booking Booking);
    public class GetBookingByCodeEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings/bookings/code/{code}", async (string code, ISender sender) =>
            {
                var result = await sender.Send(new GetBookingByCodeQuery(code));

                var response = result.Adapt<GetBookingByCodeResponse>();    

                return Results.Ok(response);
            })
            .WithName("GetBookingByCode")
            .Produces<GetBookingByCodeResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Booking By Code")
            .WithDescription("Get Booking By Code");
        }
    }
}
