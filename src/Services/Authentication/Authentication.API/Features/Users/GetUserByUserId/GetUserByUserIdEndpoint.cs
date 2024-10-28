namespace Authentication.API.Features.Users.GetUserByUserId
{
    public record GetUserByUserIdResponse(UserDto User);
    public class GetUserByUserIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/authentication/users/userid/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetUserByUserIdQuery(id));

                var response = result.Adapt<GetUserByUserIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetGuestByUserId")
            .Produces<GetUserByUserIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Guest By UserId")
            .WithDescription("Get Guest By UserId");
        }
    }
}
