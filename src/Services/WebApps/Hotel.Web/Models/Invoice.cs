namespace Hotel.Web.Models
{
    public class Invoice
    {
    }
    public record CreateInvoiceResponse(Guid InvoiceId);
    public record DeleteInvoiceResponse(bool IsSuccess, Guid BookingId);

}
