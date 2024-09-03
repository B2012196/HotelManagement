
namespace HotelManagement.API.Features.RoomStatuses.GetRoomStatuses
{
    public record GetRoomStatusesQuery() : IQuery<GetRoomStatusesResult>;
    public record GetRoomStatusesResult(IEnumerable<RoomStatus> Statuses);
    public class GetRoomStatusesHandler : IQueryHandler<GetRoomStatusesQuery, GetRoomStatusesResult>
    {
        private readonly ApplicationDbContext _context;

        public GetRoomStatusesHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetRoomStatusesResult> Handle(GetRoomStatusesQuery query, CancellationToken cancellationToken)
        {
            var statuses = await _context.RoomStatus.ToListAsync(cancellationToken);

            return new GetRoomStatusesResult(statuses);
        }
    }
}
