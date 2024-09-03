namespace HotelManagement.API.Features.Hotels.UpdateHotel
{
    public record UpdateHotelCommand
        (Guid HotelId, string Name, string Address, string Phone, string Email, int Stars, DateTime CheckinTime, DateTime CheckoutTime)
        : ICommand<UpdateHotelResult>;

    public record UpdateHotelResult(bool IsSuccess);

    public class UpdateHotelCommandHandler : ICommandHandler<UpdateHotelCommand, UpdateHotelResult>
    {
        private readonly ApplicationDbContext _context;

        public UpdateHotelCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UpdateHotelResult> Handle(UpdateHotelCommand command, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.SingleOrDefaultAsync(h => h.HotelId == command.HotelId, cancellationToken);
            if (hotel is null)
            {
                throw new HotelNotFoundException();
            }

            hotel.Name = command.Name;
            hotel.Address = command.Address;
            hotel.Phone = command.Phone;
            hotel.Email = command.Email;
            hotel.Stars = command.Stars;
            hotel.CheckinTime = command.CheckinTime;
            hotel.CheckoutTime = command.CheckoutTime;

            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateHotelResult(true);


        }
    }
}
