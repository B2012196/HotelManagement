namespace StaffManagement.API.Exceptions
{
    public class StaffRoleNotFoundException : NotFoundException
    {
        public StaffRoleNotFoundException(Guid Id) : base("StaffRole", Id)
        {

        }
    }
}
