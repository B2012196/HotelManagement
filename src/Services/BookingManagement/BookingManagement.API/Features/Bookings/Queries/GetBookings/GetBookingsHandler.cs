namespace BookingManagement.API.Features.Bookings.Queries.GetBookings
{
    public record GetBookingsQuery(int? pageNumber = 1, int? pageSize = 10, BookingStatus? filterStatus = BookingStatus.Pending) : IQuery<GetBookingsResult>;
    public record GetBookingsResult(IEnumerable<Booking> Bookings, int TotalCount);
    public class GetBookingsHandler(ApplicationDbContext context) : IQueryHandler<GetBookingsQuery, GetBookingsResult>
    {
        public async Task<GetBookingsResult> Handle(GetBookingsQuery query, CancellationToken cancellationToken)
        {

            var bookings = context.Bookings.AsQueryable();

            bookings = bookings.OrderBy(b => b.BookingId);

            if(query.filterStatus != BookingStatus.None)
            {
                bookings = bookings.Where(b => b.BookingStatus == query.filterStatus);
            }
            int totalCount = await bookings.CountAsync();

            if (query.pageNumber.HasValue && query.pageSize.HasValue)
            {
                int skip = (query.pageNumber.Value - 1) * query.pageSize.Value;
                bookings = bookings.Skip(skip).Take(query.pageSize.Value);
            }

            return new GetBookingsResult(await bookings.ToListAsync(), totalCount);
        }
    }
}
