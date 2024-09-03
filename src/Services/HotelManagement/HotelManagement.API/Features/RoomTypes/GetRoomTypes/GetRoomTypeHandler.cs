
namespace HotelManagement.API.Features.RoomTypes.GetRoomTypes
{
    public record GetRoomTypeQuery() : IQuery<GetRoomTypeResult>;
    public record GetRoomTypeResult(IEnumerable<RoomType> RoomTypes);
    public class GetRoomTypeHandler : IQueryHandler<GetRoomTypeQuery, GetRoomTypeResult>
    {
        private readonly ApplicationDbContext _context;

        public GetRoomTypeHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GetRoomTypeResult> Handle(GetRoomTypeQuery query, CancellationToken cancellationToken)
        {
            var types = await _context.RoomTypes.ToListAsync(cancellationToken);

            return new GetRoomTypeResult(types);
        }
    }
}
