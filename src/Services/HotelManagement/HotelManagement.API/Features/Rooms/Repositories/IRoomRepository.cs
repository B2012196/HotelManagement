namespace HotelManagement.API.Features.Rooms.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRooms(CancellationToken cancellationToken);
        Task<Room> GetRoomById(Guid RoomId, CancellationToken cancellationToken);
        Task<IEnumerable<Room>> GetRoomByType(Guid TypeId, CancellationToken cancellationToken);
        Task<IEnumerable<Room>> GetRoomAvaByType(Guid TypeId, CancellationToken cancellationToken);
        Task<Guid> CreateRoom(Room room, CancellationToken cancellationToken);
        Task<bool> UpdateRoom(Room room, CancellationToken cancellationToken);
        Task<bool> UpdateRoomConfirmStatus(Guid RoomId, CancellationToken cancellationToken);
        Task<bool> UpdateRoomCheckinStatus(Guid RoomId, CancellationToken cancellationToken);
        Task<bool> UpdateRoomCheckoutStatus(Guid RoomId, CancellationToken cancellationToken);
        Task<bool> DeleteRoom(Guid RoomId, CancellationToken cancellationToken);
    }
}
