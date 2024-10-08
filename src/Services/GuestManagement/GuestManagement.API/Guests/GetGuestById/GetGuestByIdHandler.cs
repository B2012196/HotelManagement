
namespace GuestManagement.API.Guests.GetGuestById
{
    public record GetGuestByIdQuery(Guid GuestId) : IQuery<GetGuestByIdResult>;
    public record GetGuestByIdResult(Guest Guest);
    public class GetGuestByIdHandler(IGuestRepository repository)
        : IQueryHandler<GetGuestByIdQuery, GetGuestByIdResult>
    {
        public async Task<GetGuestByIdResult> Handle(GetGuestByIdQuery query, CancellationToken cancellationToken)
        {
            var guest = await repository.GetGuestById(query.GuestId, cancellationToken);

            return new GetGuestByIdResult(guest);
        }
    }
}
