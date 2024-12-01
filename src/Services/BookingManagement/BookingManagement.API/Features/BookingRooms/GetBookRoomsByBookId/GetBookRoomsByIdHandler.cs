namespace BookingManagement.API.Features.BookingRooms.GetBookRoomsByBookId
{
    public record GetBookRoomsByIdQuery(Guid BookingId) : ICommand<GetBookRoomsByIdResult>;
    public record GetBookRoomsByIdResult(IEnumerable<BookingRoom> BookingRooms);
    public class GetBookRoomsByIdHandler(ApplicationDbContext context)
        : ICommandHandler<GetBookRoomsByIdQuery, GetBookRoomsByIdResult>
    {
        public async Task<GetBookRoomsByIdResult> Handle(GetBookRoomsByIdQuery query, CancellationToken cancellationToken)
        {
            var bookingrooms = await context.BookingRooms.Where(br => br.BookingId == query.BookingId).ToListAsync(cancellationToken);

            return new GetBookRoomsByIdResult(bookingrooms);
        }
    }
}
