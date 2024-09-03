namespace HotelManagement.API.Features.Hotels.GetHotels
{
    public record GetHotelsResponse(IEnumerable<Hotel> Hotels);
    public class GetHotelsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels", async (ISender sender) =>
            {
                var result = await sender.Send(new GetHotelsQuery());

                var reponse = result.Adapt<GetHotelsResponse>();

                return Results.Ok(reponse);
            })
            .WithName("GetHotels")
            .Produces<GetHotelsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Hotels")
            .WithDescription("Get Hotels");
        }
    }
}
