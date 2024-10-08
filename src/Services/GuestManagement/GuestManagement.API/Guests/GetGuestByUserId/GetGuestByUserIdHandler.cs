namespace GuestManagement.API.Guests.GetGuestByUserId
{
    public record GetGuestByUserIdQuery(Guid UserId) : IQuery<GetGuestByUserIdResult>;
    public record GetGuestByUserIdResult(Guest Guest);
    public class GetGuestByUserIdHandler(ApplicationDbContext context) 
        : IQueryHandler<GetGuestByUserIdQuery, GetGuestByUserIdResult>
    {
        public async Task<GetGuestByUserIdResult> Handle(GetGuestByUserIdQuery query, CancellationToken cancellationToken)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.UserId == query.UserId, cancellationToken);

            if(guest is null)
            {
                throw new GuestNotFoundException(query.UserId);
            }

            return new GetGuestByUserIdResult(guest);
        }
    }
}
