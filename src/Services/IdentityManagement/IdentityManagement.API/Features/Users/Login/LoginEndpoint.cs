using Microsoft.AspNetCore.Http.HttpResults;

namespace IdentityManagement.API.Features.Users.Login
{
    public record LoginRequest(string UserName, string Password);
    public record LoginResponse(TokenModel Token, bool IsSuccess);
    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginRequest request, ISender sender) =>
            {
                var command = request.Adapt<LoginCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<LoginResponse>();

                return Results.Ok(response);
            })
            .WithName("Login")
            .Produces<LoginResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Login")
            .WithDescription("Login");
        }
    }
}
