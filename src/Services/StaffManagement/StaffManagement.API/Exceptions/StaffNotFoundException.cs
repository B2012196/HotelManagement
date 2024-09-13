namespace StaffManagement.API.Exceptions
{
    public class StaffNotFoundException : NotFoundException
    {
        public StaffNotFoundException(Guid Id) : base("Staff", Id)
        {

        }
    }
}
