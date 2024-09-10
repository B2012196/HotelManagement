
namespace GuestManagement.API.Guests.DeleteGuest
{
    public record DeleteGuestResponse(bool IsSuccess);
    public class DeleteGuestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/guests/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteGuestCommand(id));

                var response = result.Adapt<DeleteGuestResponse>();

                return Results.Ok(response);

            })
            .WithName("DeleteGuest")
            .Produces<DeleteGuestResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Guest")
            .WithDescription("Delete Guest");
        }
    }
}
