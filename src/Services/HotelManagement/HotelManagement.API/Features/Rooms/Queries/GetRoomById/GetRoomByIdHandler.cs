namespace HotelManagement.API.Features.Rooms.Queries.GetRoomById
{
    public record GetRoomByIdQuery(Guid RoomId) : IQuery<GetRoomByIdResult>;
    public record GetRoomByIdResult(Room Room);
    public class GetRoomByIdHandler(IRoomRepository repository)
        : IQueryHandler<GetRoomByIdQuery, GetRoomByIdResult>
    {
        public async Task<GetRoomByIdResult> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
        {
            var room = await repository.GetRoomById(query.RoomId, cancellationToken);

            return new GetRoomByIdResult(room);
        }
    }
}
