namespace NotificationManagement.API.Models
{
    // DTO để trả về cho client
    public class NotificationDto
    {
        public string NotificationId { get; set; }  // Chuỗi trả về cho client
        public Guid GuestId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime SentAt { get; set; }
    }
}
