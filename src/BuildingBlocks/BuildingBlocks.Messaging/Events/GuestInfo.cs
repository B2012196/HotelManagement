namespace BuildingBlocks.Messaging.Events
{
    public record GuestInfoRequested : IntegrationEvent
    {
        public string UserName { get; } // Thông tin mà Guest Service dùng để tra cứu GuestId
        public GuestInfoRequested(string username)
        {
            UserName = username;
        }
    }

    public record GuestInfoResolved : IntegrationEvent
    {
        public Guid GuestId { get; }
        public GuestInfoResolved(Guid guestid)
        {
            GuestId = guestid;
        }
    }
}
