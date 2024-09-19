namespace NotificationManagement.API.Notifications.GetNotificationById
{
    public record GetNotificationsByGuestIdQuery(Guid GuestId) : IQuery<GetNotificationsByGuestIdResult>;
    public record GetNotificationsByGuestIdResult(IEnumerable<NotificationDto> NotificationDtos);
    public class GetNotificationByGuestIdHandler(IMongoDatabase mongo)
        : IQueryHandler<GetNotificationsByGuestIdQuery, GetNotificationsByGuestIdResult>
    {
        public async Task<GetNotificationsByGuestIdResult> Handle(GetNotificationsByGuestIdQuery query, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<Notification>("Notifications");

            // Lọc theo GuestId
            var filter = Builders<Notification>.Filter.Eq(n => n.GuestId, query.GuestId);

            // Tạo bản cập nhật để thay đổi trạng thái thành "Delivered"
            var update = Builders<Notification>.Update
                .Set(n => n.Status, NotificationStatus.Read);  // Giả sử bạn có trạng thái "Delivered"

            // Cập nhật tất cả các thông báo theo GuestId
            await collection.UpdateManyAsync(filter, update, cancellationToken: cancellationToken);

            var notifications = await collection.Find(filter).ToListAsync(cancellationToken);

            // Chuyển đổi danh sách Notification sang NotificationDto
            var result = notifications.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId.ToString(),  // Chuyển ObjectId thành chuỗi
                GuestId = n.GuestId,
                Title = n.Title,
                Content = n.Content,
                Status = n.Status.ToString(),  // Chuyển enum Status thành chuỗi
                SentAt = n.SentAt
            });

            return new GetNotificationsByGuestIdResult(result);
        }
    }
}
