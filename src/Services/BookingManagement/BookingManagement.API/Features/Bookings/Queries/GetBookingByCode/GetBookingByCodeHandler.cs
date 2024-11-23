
namespace BookingManagement.API.Features.Bookings.Queries.GetBookingByCode
{
    public record GetBookingByCodeQuery(string BookingCode) : IQuery<GetBookingByCodeResult>;
    public record GetBookingByCodeResult(Booking Booking);
    public class GetBookingByCodeHandler(ApplicationDbContext context) : IQueryHandler<GetBookingByCodeQuery, GetBookingByCodeResult>
    {
        public async Task<GetBookingByCodeResult> Handle(GetBookingByCodeQuery query, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.SingleOrDefaultAsync(b => b.BookingCode == query.BookingCode, cancellationToken);

            if(booking == null)
            {
                throw new BookingNotFoundException(query.BookingCode);
            }

            return new GetBookingByCodeResult(booking);
        }
    }
}
