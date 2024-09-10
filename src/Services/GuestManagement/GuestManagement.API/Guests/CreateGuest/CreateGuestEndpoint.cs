namespace GuestManagement.API.Guests.CreateGuest
{
    public record CreateGuestRequest
        (string FirstName, string LastName, DateOnly DateofBirst, string Address, string Phone, string Email);

    public record CreateGuestReponse(Guid GuestId);
    public class CreateGuestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/guests", async (CreateGuestRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateGuestCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateGuestReponse>();

                return Results.Created($"/guests/{response.GuestId}", response);
            })
            .WithName("CreateGuest")
            .Produces<CreateGuestReponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Guest")
            .WithDescription("Create Guest");
        }
    }
}
