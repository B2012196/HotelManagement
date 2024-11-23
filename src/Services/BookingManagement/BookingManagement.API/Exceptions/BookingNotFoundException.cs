namespace BookingManagement.API.Exceptions
{
    public class BookingNotFoundException : NotFoundException
    {
        public BookingNotFoundException(string Id) : base("Booking", Id)
        {

        }
    }
}
