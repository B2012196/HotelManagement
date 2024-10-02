namespace GuestManagement.API.Guests.UpdateGuest
{
    public record UpdateGuestRequest
        (Guid GuestId, Guid UserId, string FirstName, string LastName, DateOnly DateofBirst, string Address);
    public record UpdateGuestResponse(bool IsSuccess);
    public class UpdateGuestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/guests/guests", async (UpdateGuestRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateGuestCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateGuestResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateGuest")
            .Produces<UpdateGuestResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Guest")
            .WithDescription("Update Guest");
        }
    }
}
