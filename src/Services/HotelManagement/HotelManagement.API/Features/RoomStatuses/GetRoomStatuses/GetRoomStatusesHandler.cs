namespace HotelManagement.API.Features.RoomStatuses.GetRoomStatuses
{
    public record GetRoomStatusesQuery() : IQuery<GetRoomStatusesResult>;
    public record GetRoomStatusesResult(IEnumerable<RoomStatus> Statuses);
    public class GetRoomStatusesHandler(ApplicationDbContext context)
        : IQueryHandler<GetRoomStatusesQuery, GetRoomStatusesResult>
    {
        public async Task<GetRoomStatusesResult> Handle(GetRoomStatusesQuery query, CancellationToken cancellationToken)
        {
            var statuses = await context.RoomStatus.ToListAsync(cancellationToken);

            return new GetRoomStatusesResult(statuses);
        }
    }
}
