namespace HotelManagement.API.Features.Rooms.GetRooms
{
    public record GetRoomsQuery() : IQuery<GetRoomsResult>;
    public record GetRoomsResult(IEnumerable<Room> Rooms);
    public class GetRoomsHandler(ApplicationDbContext context)
        : IQueryHandler<GetRoomsQuery, GetRoomsResult>
    {
        public async Task<GetRoomsResult> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
        {
            var rooms = await context.Rooms.ToListAsync(cancellationToken);

            return new GetRoomsResult(rooms);
        }
    }
}
