namespace FinanceManagement.API.Exceptions
{
    public class OrderingNotFoundException : NotFoundException
    {
        public OrderingNotFoundException(Guid Id) : base("Ordering", Id)
        {

        }
    }
}
