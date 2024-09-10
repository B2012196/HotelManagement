namespace HotelManagement.API.Features.Hotels.CreateHotel
{
    public record CreateHotelCommand
        (string Name, string Address, string Phone, string Email, int Stars, DateTime CheckinTime, DateTime CheckoutTime)
        : ICommand<CreateHotelResult>;
    public record CreateHotelResult(Guid Id);

    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Hotel name is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Stars).GreaterThan(0).WithMessage("Stars must be greater than 0");
            RuleFor(x => x.CheckinTime).LessThan(x => x.CheckoutTime)
                .WithMessage("Check-in time must be before check-out time");
        }
    }


    public class CreateHotelHandler (ApplicationDbContext context)
        : ICommandHandler<CreateHotelCommand, CreateHotelResult>
    {
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
            context.Hotels.Add(hotel);
            await context.SaveChangesAsync(cancellationToken);
            //return result
            return new CreateHotelResult(hotel.HotelId);
        }
    }
}
