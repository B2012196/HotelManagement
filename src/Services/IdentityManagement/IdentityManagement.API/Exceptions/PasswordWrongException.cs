namespace IdentityManagement.API.Exceptions
{
    public class PasswordWrongException : BadRequestException
    {
        public PasswordWrongException(string message) : base(message)
        {
            
        }
    }
}
