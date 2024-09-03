namespace HotelManagement.API.Features.Hotels.DeleteHotel
{
    public record DeleteHotelCommand(Guid HotelId) : ICommand<DeleteHotelResult>;
    public record DeleteHotelResult(bool IsSuccess);
    public class DeleteHotelHandler : ICommandHandler<DeleteHotelCommand, DeleteHotelResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteHotelHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DeleteHotelResult> Handle(DeleteHotelCommand command, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.SingleOrDefaultAsync(h => h.HotelId == command.HotelId, cancellationToken);

            if (hotel is null)
            {
                throw new HotelNotFoundException();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteHotelResult(true);
        }
    }
}
