namespace GuestManagement.API.Guests.GetGuests
{
    public record GetGuestsQuery() : IQuery<GetGuestsResult>;
    public record GetGuestsResult(IEnumerable<Guest> Guests);
    public class GetGuestsHandler(ApplicationDbContext context)
        : IQueryHandler<GetGuestsQuery, GetGuestsResult>
    {
        public async Task<GetGuestsResult> Handle(GetGuestsQuery query, CancellationToken cancellationToken)
        {
            var guests = await context.Guests.ToListAsync(cancellationToken);
            
            return new GetGuestsResult(guests);

        }
    }
}
