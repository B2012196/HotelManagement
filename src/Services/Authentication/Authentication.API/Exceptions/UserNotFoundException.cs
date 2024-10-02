namespace Authentication.API.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userName) : base("User", userName)
        {

        }
    }
}
