using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotificationManagement.API.Models
{
    public class Notification
    {
        [BsonId]
        public ObjectId NotificationId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid GuestId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [BsonRepresentation(BsonType.String)]
        public NotificationStatus Status { get; set; }
        public DateTime SentAt { get; set; }
    }
}
