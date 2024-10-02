namespace HotelManagement.API.Exceptions
{
    public class TypeNotFoundException : NotFoundException
    {
        public TypeNotFoundException(Guid Id) : base("RoomType", Id)
        {

        }
    }
}
