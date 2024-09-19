namespace HotelManagement.API.Exceptions
{
    public class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(Guid Id) : base("Room", Id)
        {

        }
    }
}
