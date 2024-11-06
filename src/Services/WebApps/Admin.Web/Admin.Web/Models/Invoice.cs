namespace Admin.Web.Models
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public Guid BookingId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime CreateAt { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class InvoiceView
    {
        public Guid InvoiceId { get; set; }
        public string GuestName { get; set; }
        public string RoomNumber { get; set; }
        public string ServiceName { get; set; }
        public int ServiceNumber { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal TotalServiceUsed { get; set; }
        public DateTime CreateAt { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public record GetInvoicesResponse(IEnumerable<Invoice> Invoices);
    public record GetInvoiceByBookingIdResponse(Invoice Invoice);
    public record CreateInvoiceResponse(Guid InvoiceId);


}
