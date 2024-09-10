namespace BookingManagement.API.Features.Bookings.GetBookings
{
    public record GetBookingsQuery() : IQuery<GetBookingsResult>;
    public record GetBookingsResult(IEnumerable<Booking> Bookings);
    public class GetBookingsHandler(ApplicationDbContext context)
        : IQueryHandler<GetBookingsQuery, GetBookingsResult>
    {
        public async Task<GetBookingsResult> Handle(GetBookingsQuery query, CancellationToken cancellationToken)
        {
            var bookings = await context.Bookings.ToListAsync(cancellationToken);

            return new GetBookingsResult(bookings);
        }
    }
}
