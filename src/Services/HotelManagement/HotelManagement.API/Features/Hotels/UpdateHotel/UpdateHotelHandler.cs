namespace HotelManagement.API.Features.Hotels.UpdateHotel
{
    public record UpdateHotelCommand
        (Guid HotelId, string Name, string Address, string Phone, string Email, int Stars, DateTime CheckinTime, DateTime CheckoutTime)
        : ICommand<UpdateHotelResult>;

    public record UpdateHotelResult(bool IsSuccess);

    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(x => x.HotelId).NotEmpty().WithMessage("Hotel ID is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Hotel name is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Stars).GreaterThan(0).WithMessage("Stars must be greater than 0");
            RuleFor(x => x.CheckinTime).LessThan(x => x.CheckoutTime)
                .WithMessage("Check-in time must be before check-out time");
        }
    }
    public class UpdateHotelCommandHandler (ApplicationDbContext context)
        : ICommandHandler<UpdateHotelCommand, UpdateHotelResult>
    {
        public async Task<UpdateHotelResult> Handle(UpdateHotelCommand command, CancellationToken cancellationToken)
        {
            var hotel = await context.Hotels.SingleOrDefaultAsync(h => h.HotelId == command.HotelId, cancellationToken);
            if (hotel is null)
            {
                throw new HotelNotFoundException(command.HotelId);
            }

            hotel.Name = command.Name;
            hotel.Address = command.Address;
            hotel.Phone = command.Phone;
            hotel.Email = command.Email;
            hotel.Stars = command.Stars;
            hotel.CheckinTime = command.CheckinTime;
            hotel.CheckoutTime = command.CheckoutTime;

            context.Hotels.Update(hotel);
            await context.SaveChangesAsync(cancellationToken);
            return new UpdateHotelResult(true);


        }
    }
}
