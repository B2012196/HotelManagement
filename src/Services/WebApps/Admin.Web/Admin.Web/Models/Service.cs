namespace Admin.Web.Models
{
    public class Service
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public byte[]? ServiceImage { get; set; }
        public string? ContentImage { get; set; }
    }
    public record GetServicesResponse(IEnumerable<Service> Services);
    public record CreateServiceResponse(Guid ServiceId);
    public record UpdateServiceResponse(bool IsSuccess);
    public record UploadServiceImageResponse(bool IsSuccess);
    public record DeleteServiceResponse(bool IsSuccess);

}
