namespace HotelManagement.API.Features.RoomTypes.UploadRoomTypeImage
{
    public record UploadRoomTypeImageRequest(Guid TypeId, IFormFile File);
    public record UploadRoomTypeImageResponse(bool IsSuccess);
    public class UploadRoomTypeImageEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/hotels/roomtypes/{typeId}/upload-image", async (Guid typeId, IFormFile file, ISender sender) =>
            {
                //var request = new UploadRoomTypeImageRequest(typeId, file);

                //var command = request.Adapt<UploadRoomTypeImageCommand>(typeId, file);

                var result = await sender.Send(new UploadRoomTypeImageCommand(typeId, file));

                var response = result.Adapt<UploadRoomTypeImageResponse>();

                return Results.Ok(response);
            })
            .DisableAntiforgery()
            .WithName("Upload RoomTypeImage")
            .Produces<UploadRoomTypeImageResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Upload RoomTypeImage")
            .WithDescription("Upload RoomTypeImage");
        }
    }
}
