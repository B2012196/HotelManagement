
namespace Authentication.API.Features.Users.GetUserByUName
{
    public record GetUserByUNameResponse(User User);
    public class GetUserByUNameEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/authentication/users/{username}", async (string username, ISender sender) =>
            {
                var result = await sender.Send(new GetUserByUNameQuery(username));

                var response = result.Adapt<GetUserByUNameResponse>();

                return Results.Ok(response);
            })
            .WithName("GetGuestByUserId")
            .Produces<GetUserByUNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Guest By UserId")
            .WithDescription("Get Guest By UserId");
        }
    }
}
