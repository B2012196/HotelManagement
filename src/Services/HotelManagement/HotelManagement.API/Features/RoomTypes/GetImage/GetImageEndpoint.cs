namespace HotelManagement.API.Features.RoomTypes.GetImage
{
    public record GetImageResponse(byte[] ImageData, string ContentType);
    public class GetImageEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/hotels/roomtypes/{typeId}/upload-image", async (Guid typeId, ISender sender) =>
            {
                var result = await sender.Send(new GetImageQuery(typeId));

                var response = result.Adapt<GetImageResponse>();

                return Results.File(response.ImageData, response.ContentType);
            })
            .WithName("GetImageRoomTypes")
            .Produces<GetImageResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get ImageRoomTypes")
            .WithDescription("Get ImageRoomTypes");
        }
    }
}
