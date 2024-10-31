namespace FinanceManagement.API.Exceptions
{
    public class PaymentNotFoundException : NotFoundException
    {
        public PaymentNotFoundException(Guid Id) : base("Payment", Id)
        {

        }
    }
}
