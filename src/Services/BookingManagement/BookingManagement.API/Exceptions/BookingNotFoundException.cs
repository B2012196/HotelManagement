namespace BookingManagement.API.Exceptions
{
    public class BookingNotFoundException : NotFoundException
    {
        public BookingNotFoundException(Guid Id) : base("Booking", Id)
        {

        }
    }
}
