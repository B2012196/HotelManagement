
namespace HotelManagement.API.Features.Rooms.Queries.GetRoomsAvaByType
{
    public record GetRoomsAvaQuery(Guid TypeId) : IQuery<GetRoomsAvaResult>;
    public record GetRoomsAvaResult(IEnumerable<Room> Rooms);
    public class GetRoomsAvaByTypeHandler(IRoomRepository repository)
        : IQueryHandler<GetRoomsAvaQuery, GetRoomsAvaResult>
    {
        public async Task<GetRoomsAvaResult> Handle(GetRoomsAvaQuery query, CancellationToken cancellationToken)
        {
            var rooms = await repository.GetRoomAvaByType(query.TypeId, cancellationToken);

            return new GetRoomsAvaResult(rooms);
        }
    }
}
