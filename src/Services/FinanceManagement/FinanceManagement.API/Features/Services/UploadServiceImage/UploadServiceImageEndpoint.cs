namespace FinanceManagement.API.Features.Services.UploadServiceImage
{
    public record UploadServiceImageResponse(bool IsSuccess);
    public class UploadServiceImageEndpoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/finance/services/image/{id}", async (Guid id, IFormFile File, ISender sender) =>
            {
                var result = await sender.Send(new UploadServiceImageCommand(id, File));

                var response = result.Adapt<UploadServiceImageResponse>();

                return Results.Ok(response);
            })
            .DisableAntiforgery()
            .Accepts<IFormFile>("multipart/form-data")
            .WithName("UpdateServiceImage")
            .Produces<UploadServiceImageResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Service Image")
            .WithDescription("Update Service Image");
        }
    }
}
