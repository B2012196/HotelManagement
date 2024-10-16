
namespace BookingManagement.API.Features.Bookings.Queries.GetBookingsByGuestId
{
    public record GetBookingByGuestIdRQuery(Guid GuestId) : IQuery<GetBookingByGuestIdResult>;
    public record GetBookingByGuestIdResult(IEnumerable<Booking> Bookings);
    public class GetBookingByGuestIdHandler(ApplicationDbContext context)
        : IQueryHandler<GetBookingByGuestIdRQuery, GetBookingByGuestIdResult>
    {
        public async Task<GetBookingByGuestIdResult> Handle(GetBookingByGuestIdRQuery query, CancellationToken cancellationToken)
        {
            var bookings = await context.Bookings.Where(b => b.GuestId == query.GuestId).ToListAsync(cancellationToken);

            if(bookings is null)
            {
                throw new BookingNotFoundException(query.GuestId);
            }

            return new GetBookingByGuestIdResult(bookings);
        }
    }
}
