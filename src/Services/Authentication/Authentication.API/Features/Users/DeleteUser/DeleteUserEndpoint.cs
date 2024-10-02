namespace Authentication.API.Features.Users.DeleteUser
{
    public record DeleteUserResponse(bool IsSuccess);
    public class DeleteUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/authentication/users/{username}", async (string username, ISender sender) =>
            {
                var result = await sender.Send(new DeleteUserCommand(username));

                var response = result.Adapt<DeleteUserResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteUser")
            .Produces<DeleteUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete User")
            .WithDescription("Delete User");
        }
    }
}
