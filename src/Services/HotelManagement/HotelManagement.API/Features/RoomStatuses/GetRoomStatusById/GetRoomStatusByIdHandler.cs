
namespace HotelManagement.API.Features.RoomStatuses.GetRoomStatusById
{
    public record GetRoomStatusByIdQuery(Guid StatusId) : IQuery<GetRoomStatusByIdResult>;
    public record GetRoomStatusByIdResult(RoomStatus RoomStatus);
    public class GetRoomStatusByIdHandler(ApplicationDbContext context)
        : IQueryHandler<GetRoomStatusByIdQuery, GetRoomStatusByIdResult>
    {
        public async Task<GetRoomStatusByIdResult> Handle(GetRoomStatusByIdQuery query, CancellationToken cancellationToken)
        {
            var status = await context.RoomStatus.SingleOrDefaultAsync(s => s.StatusId == query.StatusId, cancellationToken);
            if (status is null)
            {
                throw new RoomStatusNotFoundException(query.StatusId);
            }

            return new GetRoomStatusByIdResult(status);
        }
    }
}
