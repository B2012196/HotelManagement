namespace FinanceManagement.API.Exceptions
{
    public class ServiceNotFoundException : NotFoundException
    {
        public ServiceNotFoundException(Guid Id) : base("Service", Id)
        {

        }
    }
}
