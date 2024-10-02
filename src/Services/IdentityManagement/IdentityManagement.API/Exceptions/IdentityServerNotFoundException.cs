namespace IdentityManagement.API.Exceptions
{
    public class IdentityServerNotFoundException : NotFoundException
    {
        public IdentityServerNotFoundException(string name) : base(name)
        {

        }
    }
}
