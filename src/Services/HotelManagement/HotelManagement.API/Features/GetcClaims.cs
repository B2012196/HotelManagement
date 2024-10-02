namespace HotelManagement.API.Features
{
    public class GetcClaims : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/identity", (HttpContext context) => {
                var claims = from c in context.User.Claims
                             select new { c.Type, c.Value };

                return Results.Json(claims);
            })
            .WithName("GetUserClaims")
            .Produces(StatusCodes.Status200OK)
            .RequireAuthorization(); // Optional: Require that the user is authenticated
        }
    }
}
