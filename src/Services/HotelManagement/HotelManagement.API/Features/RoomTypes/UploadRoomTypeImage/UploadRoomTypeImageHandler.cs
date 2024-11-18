namespace HotelManagement.API.Features.RoomTypes.UploadRoomTypeImage
{
    public record UploadRoomTypeImageCommand(Guid TypeId, IFormFile File) : ICommand<UploadRoomTypeImageResult>;
    public record UploadRoomTypeImageResult(bool IsSuccess);
    public class UploadRoomTypeImageHandler(ApplicationDbContext context)
        : ICommandHandler<UploadRoomTypeImageCommand, UploadRoomTypeImageResult>
    {
        public async Task<UploadRoomTypeImageResult> Handle(UploadRoomTypeImageCommand command, CancellationToken cancellationToken)
        {
            //find typeroom
            var type = await context.RoomTypes.SingleOrDefaultAsync(t => t.TypeId == command.TypeId, cancellationToken);

            if (type is null)
            {
                throw new TypeNotFoundException(command.TypeId);
            }

            var typeImage = new Image
            {
                ImageId = Guid.NewGuid(),
                RoomTypeId = type.TypeId
            };

            var file = command.File;
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    // Lưu dữ liệu ảnh vào RoomType
                    typeImage.Data = memoryStream.ToArray();  // Chuyển file thành mảng byte
                    typeImage.ContentType = file.ContentType;  // Lưu loại nội dung của ảnh
                }
            }

            context.Images.Add(typeImage);
            await context.SaveChangesAsync(cancellationToken);

            return new UploadRoomTypeImageResult(true);  // Trả về kết quả thành công
        }
    }
}
