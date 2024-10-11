
namespace HotelManagement.API.Features.RoomTypes.GetRoomTypeById
{
    public record GetRoomTypeByIdQuery(Guid TypeId) : IQuery<GetRoomTypeByIdResult>;
    public record GetRoomTypeByIdResult(RoomType RoomType);
    public class GetRoomTypeByIdHandler(ApplicationDbContext context) 
        : IQueryHandler<GetRoomTypeByIdQuery, GetRoomTypeByIdResult>
    {
        public async Task<GetRoomTypeByIdResult> Handle(GetRoomTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var type = await context.RoomTypes.SingleOrDefaultAsync(t => t.TypeId == query.TypeId,cancellationToken);
            if(type is null)
            {
                throw new TypeNotFoundException(query.TypeId);
            }

            return new GetRoomTypeByIdResult(type);
        }
    }
}
