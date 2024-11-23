
namespace BookingManagement.API.Features.Bookings.Queries.GetBookingsByStatus
{
    public record GetBookingsByStatusQuery(BookingStatus BookingStatus) : IQuery<GetBookingsByStatusResult>;
    public record GetBookingsByStatusResult(IEnumerable<Booking> Bookings);
    public class GetBookingsByStatusHandler(ApplicationDbContext context) : IQueryHandler<GetBookingsByStatusQuery, GetBookingsByStatusResult>
    {
        public async Task<GetBookingsByStatusResult> Handle(GetBookingsByStatusQuery query, CancellationToken cancellationToken)
        {
            var bookings = await context.Bookings.Where(b => b.BookingStatus == query.BookingStatus).ToListAsync(cancellationToken);

            return new GetBookingsByStatusResult(bookings);
        }
    }
}
