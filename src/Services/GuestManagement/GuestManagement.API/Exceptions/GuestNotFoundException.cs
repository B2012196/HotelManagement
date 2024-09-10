namespace GuestManagement.API.Exceptions
{
    public class GuestNotFoundException : NotFoundException
    {
        public GuestNotFoundException(Guid Id) : base("Guest", Id)
        {

        }
    }
}
