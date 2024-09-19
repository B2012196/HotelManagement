
using FluentValidation;

namespace GuestManagement.API.Guests.UpdateGuest
{
    public record UpdateGuestCommand
        (Guid GuestId, string FirstName, string LastName, DateOnly DateofBirst, string Address, string Phone, string Email)
        : ICommand<UpdateGuestResult>;
    public record UpdateGuestResult(bool IsSuccess);

    public class UpdateGuestCommandValidator : AbstractValidator<UpdateGuestCommand>
    {
        public UpdateGuestCommandValidator()
        {
            RuleFor(x => x.GuestId).NotEmpty().WithMessage("Guest ID is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.DateofBirst).NotEmpty().WithMessage("Date of birth is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required").Matches(@"^\d+$")
                .WithMessage("Phone number must be numeric");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
        }
    }
    public class UpdateGuestHandler(ApplicationDbContext context)
        : ICommandHandler<UpdateGuestCommand, UpdateGuestResult>
    {
        public async Task<UpdateGuestResult> Handle(UpdateGuestCommand command, CancellationToken cancellationToken)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.GuestId == command.GuestId, cancellationToken);
            if (guest is null)
            {
                throw new GuestNotFoundException(command.GuestId);
            }
            
            guest.FirstName = command.FirstName;
            guest.LastName = command.LastName;
            guest.DateofBirst = command.DateofBirst;
            guest.Address = command.Address;

            //save database
            context.Guests.Update(guest);
            await context.SaveChangesAsync(cancellationToken);

            //return
            return new UpdateGuestResult(true);
        }
    }
}
