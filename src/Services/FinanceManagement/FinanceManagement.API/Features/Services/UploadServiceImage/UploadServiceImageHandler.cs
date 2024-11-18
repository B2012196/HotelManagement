
namespace FinanceManagement.API.Features.Services.UploadServiceImage
{
    public record UploadServiceImageCommand(Guid ServiceId, IFormFile File) : ICommand<UploadServiceImageResult>;
    public record UploadServiceImageResult(bool IsSuccess);
    public class UploadServiceImageHandler(ApplicationDbContext context)
        : ICommandHandler<UploadServiceImageCommand, UploadServiceImageResult>
    {
        public async Task<UploadServiceImageResult> Handle(UploadServiceImageCommand command, CancellationToken cancellationToken)
        {
            var service = await context.Services.SingleOrDefaultAsync(s => s.ServiceId == command.ServiceId);

            if (service == null)
            {
                throw new ServiceNotFoundException(command.ServiceId);
            }

            var file = command.File;
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    // Lưu dữ liệu ảnh vào RoomType
                    service.ServiceImage = memoryStream.ToArray();  // Chuyển file thành mảng byte
                    service.ContentImage = file.ContentType;  // Lưu loại nội dung của ảnh
                }
            }

            context.Services.Update(service);
            await context.SaveChangesAsync(cancellationToken);

            return new UploadServiceImageResult(true);

        }
    }
}
