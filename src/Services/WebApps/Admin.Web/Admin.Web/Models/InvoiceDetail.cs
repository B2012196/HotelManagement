namespace Admin.Web.Models
{
    public class InvoiceDetail
    {
        public Guid InvoiceId { get; set; }
        public Guid ServiceId { get; set; }
        public int Numberofservice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public record GetInvoiceDetailsResponse(IEnumerable<InvoiceDetail> InvoiceDetails);
    public record CreateInvoiceDetailResponse(bool IsSuccess);

}
