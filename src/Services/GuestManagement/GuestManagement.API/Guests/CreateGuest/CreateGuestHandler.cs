using FluentValidation;

namespace GuestManagement.API.Guests.CreateGuest
{
    public record CreateGuestCommand
        (string FirstName, string LastName, DateOnly DateofBirst, string Address, string Phone, string Email) 
        : ICommand<CreateGuestResult>;

    public record CreateGuestResult(Guid GuestId);

    public class CreateGuestCommandValidator : AbstractValidator<CreateGuestCommand>
    {
        public CreateGuestCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.DateofBirst)
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid");

            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
        }
        // Hàm kiểm tra DateOnly có hợp lệ không
        private bool BeAValidDate(DateOnly date)
        {
            return date != default;
        }
    }
    public class CreateGuestHandler(ApplicationDbContext context)
        : ICommandHandler<CreateGuestCommand, CreateGuestResult>
    {
        public async Task<CreateGuestResult> Handle(CreateGuestCommand command, CancellationToken cancellationToken)
        {
            var guest = new Guest
            {
                GuestId = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                DateofBirst = command.DateofBirst,
                Address = command.Address,
            };

            //save database
            context.Guests.Add(guest);
            await context.SaveChangesAsync(cancellationToken);

            //return
            return new CreateGuestResult(guest.GuestId);

        }
    }
}
