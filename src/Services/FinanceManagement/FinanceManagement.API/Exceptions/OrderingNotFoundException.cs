namespace FinanceManagement.API.Exceptions
{
    public class InvoiceNotFoundException : NotFoundException
    {
        public InvoiceNotFoundException(Guid Id) : base("Invoice", Id)
        {

        }
    }
}
