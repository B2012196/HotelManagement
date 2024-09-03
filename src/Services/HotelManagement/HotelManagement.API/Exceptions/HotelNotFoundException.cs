namespace HotelManagement.API.Exceptions
{
    public class HotelNotFoundException : Exception
    {
        public HotelNotFoundException() : base("Hotel not found!")
        {
            
        }
    }
}
