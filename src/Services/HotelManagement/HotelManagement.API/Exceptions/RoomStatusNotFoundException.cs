namespace HotelManagement.API.Exceptions
{
    public class RoomStatusNotFoundException : NotFoundException
    {
        public RoomStatusNotFoundException(Guid Id) : base("RoomStatus", Id)
        {

        }
    }
}
