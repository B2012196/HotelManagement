namespace BuildingBlocks.Messaging.Events
{
    public record InvoiceTotalPriceEvent : IntegrationEvent
    {
        public Guid BookingId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
 