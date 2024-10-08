namespace GuestManagement.API.Guests.GetGuestByUserId
{
    public record GetGuestByUserIdReponse(Guest Guest);
    public class GetGuestByUserIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/guests/guests/userid/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetGuestByUserIdQuery(id));

                var response = result.Adapt<GetGuestByUserIdReponse>();

                return Results.Ok(response);
            })
            .WithName("GetGuestByUserId")
            .Produces<GetGuestByUserIdReponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Guest By UserId")
            .WithDescription("Get Guest By UserId");
        }
    }
}
