
namespace HotelManagement.API.Features.Rooms.Queries.GetRoomsAva
{
    public record GetRoomsAvaQuery() : IQuery<GetRoomsAvaResult>;
    public record GetRoomsAvaResult(IEnumerable<Room> Rooms);
    public class GetRoomsAvaHandler(ApplicationDbContext context) : IQueryHandler<GetRoomsAvaQuery, GetRoomsAvaResult>
    {
        public async Task<GetRoomsAvaResult> Handle(GetRoomsAvaQuery query, CancellationToken cancellationToken)
        {
            var rooms = await context.Rooms.Where(r => r.StatusId == Guid.Parse("3ad2b5c4-cd33-42f1-a030-53a0a213f791")).ToListAsync(cancellationToken); 

            return new GetRoomsAvaResult(rooms);
        }
    }
}
