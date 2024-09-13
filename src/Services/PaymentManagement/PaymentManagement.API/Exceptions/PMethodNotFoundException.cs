namespace PaymentManagement.API.Exceptions
{
    public class PMethodNotFoundException : NotFoundException
    {
        public PMethodNotFoundException(Guid Id) : base("PaymentMethod", Id)
        {

        }
    }
}
