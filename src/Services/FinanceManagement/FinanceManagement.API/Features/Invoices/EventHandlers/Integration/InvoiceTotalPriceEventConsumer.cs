namespace FinanceManagement.API.Features.Invoices.EventHandlers.Integration
{
    public class InvoiceTotalPriceEventConsumer(IInvoiceRepository invoiceRepository) : IConsumer<InvoiceTotalPriceEvent>
    {
        public async Task Consume(ConsumeContext<InvoiceTotalPriceEvent> context)
        {
            var eventMessage = context.Message;
            await invoiceRepository.UpdateInvoiceTotal(eventMessage.BookingId, eventMessage.TotalPrice, context.CancellationToken);
        }
    }
}
