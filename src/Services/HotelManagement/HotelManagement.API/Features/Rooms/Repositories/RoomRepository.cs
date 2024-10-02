namespace HotelManagement.API.Features.Rooms.Repositories
{
    public class RoomRepository(ApplicationDbContext context)
        : IRoomRepository
    {
        public async Task<Guid> CreateRoom(Room room, CancellationToken cancellationToken)
        {
            context.Rooms.Add(room);
            await context.SaveChangesAsync(cancellationToken);
            return room.RoomId;
        }

        public async Task<bool> DeleteRoom(Guid RoomId, CancellationToken cancellationToken)
        {
            var room = await context.Rooms.SingleOrDefaultAsync(r => r.RoomId == RoomId, cancellationToken);
            if (room is null)
            {
                throw new RoomNotFoundException(RoomId);
            }

            context.Rooms.Remove(room);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Room> GetRoomById(Guid RoomId, CancellationToken cancellationToken)
        {
            var room = await context.Rooms.SingleOrDefaultAsync(r => r.RoomId == RoomId, cancellationToken);
            if (room is null)
            {
                throw new RoomNotFoundException(RoomId);
            }

            return room;
        }

        public async Task<IEnumerable<Room>> GetRooms(CancellationToken cancellationToken)
        {
            var rooms = await context.Rooms.ToListAsync(cancellationToken);

            return rooms;
        }

        public async Task<bool> UpdateRoom(Room room, CancellationToken cancellationToken)
        {
            
            context.Rooms.Update(room);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateRoomStatus(Guid RoomId, CancellationToken cancellationToken)
        {
            var room = await context.Rooms.SingleOrDefaultAsync(r => r.RoomId == RoomId, cancellationToken);
            if (room is null)
            {
                throw new RoomNotFoundException(RoomId);
            }

            var availableGuidString = "3ad2b5c4-cd33-42f1-a030-53a0a213f791";         
            var availableGuid = StringParseGuid(availableGuidString);


            if (room.StatusId != availableGuid)
            {
                throw new BadRequestException($"Room {room.RoomId} is not available for confirm.");
            }

            var confirmGuidString = "c565efa3-3408-481e-8c5b-95fd950810f5";
            var confirmGuid = StringParseGuid(confirmGuidString);

            room.StatusId = confirmGuid;

            context.Rooms.Update(room);
            await context.SaveChangesAsync(cancellationToken);
            return true;

        }

        private Guid StringParseGuid(string str)
        {
            if (!Guid.TryParse(str, out var confirmGuid))
            {
                throw new InvalidOperationException($"Invalid GUID format: {str}");
            }

            return confirmGuid;
        }
    }
}
