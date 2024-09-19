namespace HotelManagement.API.Features.Rooms.Queries.GetRooms
{
    public record GetRoomsQuery() : IQuery<GetRoomsResult>;
    public record GetRoomsResult(IEnumerable<Room> Rooms);
    public class GetRoomsHandler(IRoomRepository repository)
        : IQueryHandler<GetRoomsQuery, GetRoomsResult>
    {
        public async Task<GetRoomsResult> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
        {
            var rooms = await repository.GetRooms(cancellationToken);

            return new GetRoomsResult(rooms);
        }
    }
}
