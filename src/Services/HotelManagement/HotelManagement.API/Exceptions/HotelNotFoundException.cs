namespace HotelManagement.API.Exceptions
{
    public class HotelNotFoundException : NotFoundException
    {
        public HotelNotFoundException(Guid Id) : base("Hotel", Id)
        {
            
        }
    }
}
