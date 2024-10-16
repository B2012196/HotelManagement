
namespace Authentication.API.Features.Users.UpdatePassword
{
    public record UpdatePasswordRequest(Guid UserId, string Password, string NewPassword);
    public record UpdatePasswordResponse(bool IsSuccess);
    public class UpdatePasswordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/authentication/users/password", async(UpdatePasswordRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdatePasswordCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdatePasswordResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdatePassword")
            .Produces<UpdatePasswordResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Password")
            .WithDescription("Update Password");
        }
    }
}
