namespace Authentication.API.Features.Users.UpdateUser
{
    public record UpdateUserRequest(Guid RoleId, string UserName, string Password, string Email, string PhoneNumber, bool IsActive);
    public record UpdateUserResponse(bool IsSuccess);
    public class UpdateUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/authentication/users", async (UpdateUserRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateUserCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateUserResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateUser")
            .Produces<UpdateUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update User")
            .WithDescription("Update User");
        }
    }
}
