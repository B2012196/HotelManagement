﻿namespace Authentication.API.Features.Users.GetUsers
{
    public record GetUsersResponse(IEnumerable<UserDto> UserDtos);
    public class GetUsersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/authentication/users", async (ISender sender) =>
            {
                var result = await sender.Send(new GetUsersQuery());

                var response = result.Adapt<GetUsersResponse>();

                return Results.Ok(response);
            })
            .RequireAuthorization("RequireAdminRole")
            .WithName("GetUsers")
            .Produces<GetUsersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Users")
            .WithDescription("Get Users");
        }
    }
}
