
namespace BookingManagement.API.Features.Bookings.Queries.GetBookingById
{
    public record GetBookingByIdQuery(Guid BookingId) : IQuery<GetBookingByIdResult>;
    public record GetBookingByIdResult(Booking Booking);
    public class GetBookingByIdHandler : IQueryHandler<GetBookingByIdQuery, GetBookingByIdResult>
    {
        public Task<GetBookingByIdResult> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
