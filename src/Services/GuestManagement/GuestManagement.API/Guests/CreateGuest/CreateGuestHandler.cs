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

            RuleFor(x => x.DateofBirst).NotEmpty().WithMessage("Date of birth is required");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required").Matches(@"^\d+$")
                .WithMessage("Phone number must be numeric");

            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
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
                Phone = command.Phone,
                Email = command.Email
            };

            //save database
            context.Guests.Add(guest);
            await context.SaveChangesAsync(cancellationToken);

            //return
            return new CreateGuestResult(guest.GuestId);

        }
    }
}
