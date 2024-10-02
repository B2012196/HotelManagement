using GuestManagement.API.Guests.Repository;

namespace GuestManagement.API.Guests.GetGuests
{
    public record GetGuestsQuery() : IQuery<GetGuestsResult>;
    public record GetGuestsResult(IEnumerable<Guest> Guests);
    public class GetGuestsHandler(IGuestRepository repository)
        : IQueryHandler<GetGuestsQuery, GetGuestsResult>
    {
        public async Task<GetGuestsResult> Handle(GetGuestsQuery query, CancellationToken cancellationToken)
        {
            var guests = await repository.GetGuests(cancellationToken);
            return new GetGuestsResult(guests);

        }
    }
}
