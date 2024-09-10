namespace HotelManagement.API.Features.Hotels.GetHotels
{
    public record GetHotelsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetHotelsResponse(IEnumerable<Hotel> Hotels);
    public class GetHotelsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels", async ([AsParameters] GetHotelsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetHotelsQuery>();

                var result = await sender.Send(query);

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
