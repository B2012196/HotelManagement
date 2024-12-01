namespace HotelManagement.API.Features.Rooms.Queries.GetRooms
{
    public record GetRoomsQuery(int? pageNumber = 1, int? pageSize = 10) : IQuery<GetRoomsResult>;
    public record GetRoomsResult(IEnumerable<Room> Rooms, int TotalCount);
    public class GetRoomsHandler(ApplicationDbContext context)
        : IQueryHandler<GetRoomsQuery, GetRoomsResult>
    {
        public async Task<GetRoomsResult> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
        {
            var rooms = context.Rooms.AsQueryable();

            rooms = rooms.OrderBy(r => r.RoomId);

            int totalCount = await rooms.CountAsync();

            if (query.pageNumber.HasValue && query.pageSize.HasValue)
            {
                int skip = (query.pageNumber.Value - 1) * query.pageSize.Value;
                rooms = rooms.Skip(skip).Take(query.pageSize.Value);
            }

            return new GetRoomsResult(rooms, totalCount);
        }
    }
}
