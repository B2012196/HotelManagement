namespace FinanceManagement.API.Exceptions
{
    public class PayMethodNotFoundException : NotFoundException
    {
        public PayMethodNotFoundException(Guid Id) : base("PaymentMethod", Id)
        {

        }
    }
}
