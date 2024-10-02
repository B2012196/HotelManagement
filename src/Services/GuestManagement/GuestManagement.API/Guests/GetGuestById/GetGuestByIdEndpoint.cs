namespace GuestManagement.API.Guests.GetGuestById
{
    public record GetGuestByIdReponse(Guest Guest);
    public class GetGuestByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/guests/guests/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetGuestByIdQuery(id));

                var response = result.Adapt<GetGuestByIdReponse>();

                return Results.Ok(response);
            })
            .WithName("GetGuestById")
            .Produces<GetGuestByIdReponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Guest By Id")
            .WithDescription("Get Guest By Id");
        }
    }
}
