namespace HotelManagement.API.Features.Hotels.CreateHotel
{
    public record CreateHotelCommand
        (string Name, string Address, string Phone, string Email, int Stars, DateTime CheckinTime, DateTime CheckoutTime)
        : ICommand<CreateHotelResult>;
    public record CreateHotelResult(Guid Id);
    public class CreateHotelHandler : ICommandHandler<CreateHotelCommand, CreateHotelResult>
    {
        private readonly ApplicationDbContext _context;

        public CreateHotelHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreateHotelResult> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
        {
            //create Hotel entity
            var hotel = new Hotel
            {
                HotelId = Guid.NewGuid(),
                Name = command.Name,
                Address = command.Address,
                Phone = command.Phone,
                Email = command.Email,
                Stars = command.Stars,
                CheckinTime = command.CheckinTime,
                CheckoutTime = command.CheckoutTime
            };

            //save database
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync(cancellationToken);
            //return result
            return new CreateHotelResult(hotel.HotelId);
        }
    }
}
