
namespace GuestManagement.API.Guests.GetGuestById
{
    public record GetGuestByIdResponse(Guest Guest);
    public class GetGuestByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/guests/guests/id/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetGuestByIdQuery(id));

                var response = result.Adapt<GetGuestByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetGuestById")
            .Produces<GetGuestByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Guest By Id")
            .WithDescription("Get Guest By Id");
        }
    }
}
