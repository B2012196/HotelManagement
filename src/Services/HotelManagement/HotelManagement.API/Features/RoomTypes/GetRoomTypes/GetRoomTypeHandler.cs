
namespace HotelManagement.API.Features.RoomTypes.GetRoomTypes
{
    public record GetRoomTypeQuery() : IQuery<GetRoomTypeResult>;
    public record GetRoomTypeResult(IEnumerable<RoomType> RoomTypes);
    public class GetRoomTypeHandler(ApplicationDbContext context)
        : IQueryHandler<GetRoomTypeQuery, GetRoomTypeResult>
    {
        public async Task<GetRoomTypeResult> Handle(GetRoomTypeQuery query, CancellationToken cancellationToken)
        {
            var types = await context.RoomTypes.ToListAsync(cancellationToken);

            return new GetRoomTypeResult(types);
        }
    }
}
