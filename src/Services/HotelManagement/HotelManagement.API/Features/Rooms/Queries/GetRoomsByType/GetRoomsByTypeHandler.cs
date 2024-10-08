
namespace HotelManagement.API.Features.Rooms.Queries.GetRoomsByType
{
    public record GetRoomsByTypeQuery(Guid TypeId) : IQuery<GetRoomsByTypeResult>;
    public record GetRoomsByTypeResult(IEnumerable<Room> Rooms);
    public class GetRoomsByTypeHandler(IRoomRepository repository)
        : IQueryHandler<GetRoomsByTypeQuery, GetRoomsByTypeResult>
    {
        public async Task<GetRoomsByTypeResult> Handle(GetRoomsByTypeQuery query, CancellationToken cancellationToken)
        {
            var rooms = await repository.GetRoomByType(query.TypeId, cancellationToken);

            return new GetRoomsByTypeResult(rooms);
        }
    }
}
