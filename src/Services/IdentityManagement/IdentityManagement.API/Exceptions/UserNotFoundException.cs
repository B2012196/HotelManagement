namespace IdentityManagement.API.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string Username) : base("User", Username)
        {

        }
    }
}
