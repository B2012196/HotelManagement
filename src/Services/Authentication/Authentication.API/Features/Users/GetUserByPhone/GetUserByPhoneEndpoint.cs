namespace Authentication.API.Features.Users.GetUserByPhone
{
    public record GetUserByPhoneResponse(UserDto UserDto);
    public class GetUserByPhoneEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/authentication/users/phone/{phone}", async (string phone, ISender sender) =>
            {
                var result = await sender.Send(new GetUserByPhoneQuery(phone));

                var response = result.Adapt<GetUserByPhoneResponse>();

                return Results.Ok(response);
            })
            .WithName("GetUserByPhone")
            .Produces<GetUserByPhoneResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get User By Phone")
            .WithDescription("Get User By Phone");
        }
    }
}
