
namespace NotificationManagement.API.Notifications.GetNotifications
{
    public record GetNotificationsQuery() : IQuery<GetNotificationsResult>;
    public record GetNotificationsResult(IEnumerable<NotificationDto> NotificationDtos);
    public  class GetNotificationsHandler(IMongoDatabase mongo)
        : IQueryHandler<GetNotificationsQuery, GetNotificationsResult>
    {
        public async Task<GetNotificationsResult> Handle(GetNotificationsQuery query, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<Notification>("Notifications");

            var notifications =  await collection.Find(_ => true).ToListAsync(cancellationToken);

            // Trả về GetNotificationsResult với NotificationId dưới dạng chuỗi
            var result = notifications.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId.ToString(),  // Chuyển đổi ObjectId thành chuỗi
                GuestId = n.GuestId,
                Title = n.Title,
                Content = n.Content,
                Status = n.Status.ToString(),
                SentAt = n.SentAt
            });

            return new GetNotificationsResult(result);
        }
    }
}
