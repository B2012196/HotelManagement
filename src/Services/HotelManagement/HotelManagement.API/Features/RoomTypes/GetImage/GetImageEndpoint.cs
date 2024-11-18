namespace HotelManagement.API.Features.RoomTypes.GetImage
{
    public record GetImageResponse(IEnumerable<Image> Images);
    public class GetImageEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/roomtypes/image", async (ISender sender) =>
            {
                var result = await sender.Send(new GetImageQuery());

                var response = result.Adapt<GetImageResponse>();

                return Results.Ok(response);
            })
            .WithName("GetImageRoomTypes")
            .Produces<GetImageResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get ImageRoomTypes")
            .WithDescription("Get ImageRoomTypes");
        }
    }
}
