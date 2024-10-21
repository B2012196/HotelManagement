namespace HotelManagement.API.Features.RoomTypes.UploadRoomTypeImage
{
    public record UploadRoomTypeImageRequest(Guid TypeId, IFormFile File);
    public record UploadRoomTypeImageResponse(bool IsSuccess);
    public class UploadRoomTypeImageEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/hotels/roomtypes/upload-image/{TypeId}", async (Guid TypeId, IFormFile File, ISender sender) =>
            {
                var result = await sender.Send(new UploadRoomTypeImageCommand(TypeId, File));

                var response = result.Adapt<UploadRoomTypeImageResponse>();

                return Results.Ok(response);
            })
            .DisableAntiforgery()
            .Accepts<IFormFile>("multipart/form-data")
            .WithName("Upload RoomTypeImage")
            .Produces<UploadRoomTypeImageResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Upload RoomTypeImage")
            .WithDescription("Upload RoomTypeImage");
        }
    }
}
