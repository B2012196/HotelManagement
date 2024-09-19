using MongoDB.Bson;

namespace NotificationManagement.API.Notifications.CreateNotification
{
    public record CreateNotificationCommand
        (Guid GuestId, string Title, string Content) : ICommand<CreateNotificationResult>;
    public record CreateNotificationResult(string NotificationId);
    public class CreateNotificationHandler(IMongoDatabase mongo)
        : ICommandHandler<CreateNotificationCommand, CreateNotificationResult>
    {
        public async Task<CreateNotificationResult> Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<Notification>("Notifications");

            var notification = new Notification
            {
                NotificationId = ObjectId.GenerateNewId(),
                GuestId = command.GuestId,
                Title = command.Title,
                Content = command.Content,
                Status = NotificationStatus.Sent,
                SentAt = DateTime.Now,
            };

            await collection.InsertOneAsync(notification, cancellationToken: cancellationToken);

            return new CreateNotificationResult(notification.NotificationId.ToString());

        }
    }
}
