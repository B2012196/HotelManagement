
namespace BookingManagement.API.Features.BookingRooms.GetBookingRooms
{
    public record GetBookingRoomsQuery : IQuery<GetBookingRoomsResult>;
    public record GetBookingRoomsResult(IEnumerable<BookingRoom> BookingRooms);
    public class GetBookingRoomsHandler(ApplicationDbContext context)
        : IQueryHandler<GetBookingRoomsQuery, GetBookingRoomsResult>
    {
        public async Task<GetBookingRoomsResult> Handle(GetBookingRoomsQuery query, CancellationToken cancellationToken)
        {
            var bookingrooms = await context.BookingRooms.ToListAsync(cancellationToken);

            return new GetBookingRoomsResult(bookingrooms);
        }
    }
}
