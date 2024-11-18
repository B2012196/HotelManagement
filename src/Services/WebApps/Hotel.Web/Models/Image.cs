namespace Hotel.Web.Models
{
    public class Image
    {
        public Guid ImageId { get; set; } // Unique identifier for the image
        public byte[] Data { get; set; } // Binary data of the image
        public string ContentType { get; set; } // MIME type (e.g., "image/jpeg")
        public Guid RoomTypeId { get; set; } // Foreign key to RoomType
    }

    public record GetImageResponse(IEnumerable<Image> Images);
}
