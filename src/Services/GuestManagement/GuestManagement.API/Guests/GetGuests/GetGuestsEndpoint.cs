
namespace GuestManagement.API.Guests.GetGuests
{
    public record GetGuestsResponse(IEnumerable<Guest> Guests);
    public class GetGuestsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/guests/guests", async (ISender sender) =>
            {
                var result = await sender.Send(new GetGuestsQuery());

                var response = result.Adapt<GetGuestsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetGuests")
            .Produces<GetGuestsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Guests")
            .WithDescription("Get Guests");
        }
    }
}
