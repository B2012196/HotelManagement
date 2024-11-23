
namespace BookingManagement.API.Features.Bookings.Queries.GetBookingById
{
    public record GetBookingByIdQuery(Guid BookingId) : IQuery<GetBookingByIdResult>;
    public record GetBookingByIdResult(Booking Booking);
    public class GetBookingByIdHandler(ApplicationDbContext context) : IQueryHandler<GetBookingByIdQuery, GetBookingByIdResult>
    {
        public async Task<GetBookingByIdResult> Handle(GetBookingByIdQuery query, CancellationToken cancellationToken)
        {
            var booking = await context.Bookings.SingleOrDefaultAsync(b => b.BookingId == query.BookingId, cancellationToken);

            if (booking == null)
            {
                throw new BookingNotFoundException(query.BookingId+"");
            }

            return new GetBookingByIdResult(booking);
        }
    }
}
