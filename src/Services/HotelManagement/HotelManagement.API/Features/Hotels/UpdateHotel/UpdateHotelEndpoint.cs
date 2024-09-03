namespace HotelManagement.API.Features.Hotels.UpdateHotel
{
    public record UpdateHotelRequest(Guid HotelId, string Name, string Address, string Phone,
        string Email, int Stars, DateTime CheckinTime, DateTime CheckoutTime);

    public record UpdateHotelResponse(bool IsSuccess);

    public class UpdateHotelEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/hotels", async (UpdateHotelRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateHotelCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<UpdateHotelResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateHotel")
            .Produces<UpdateHotelResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Hotel")
            .WithDescription("Update Hotel");
        }
    }
}
