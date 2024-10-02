
namespace GuestManagement.API.Guests.GetGuestById
{
    public record GetGuestByIdQuery(Guid GuestId) : IQuery<GetGuestByIdResult>;
    public record GetGuestByIdResult(Guest Guest);
    public class GetGuestByIdHandler(ApplicationDbContext context) 
        : IQueryHandler<GetGuestByIdQuery, GetGuestByIdResult>
    {
        public async Task<GetGuestByIdResult> Handle(GetGuestByIdQuery query, CancellationToken cancellationToken)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.GuestId == query.GuestId, cancellationToken);

            if(guest is null)
            {
                throw new GuestNotFoundException(query.GuestId);
            }

            return new GetGuestByIdResult(guest);
        }
    }
}
