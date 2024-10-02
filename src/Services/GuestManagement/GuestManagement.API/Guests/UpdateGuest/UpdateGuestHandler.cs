namespace GuestManagement.API.Guests.UpdateGuest
{
    public record UpdateGuestCommand
        (Guid GuestId, Guid UserId, string FirstName, string LastName, DateOnly DateofBirst, string Address)
        : ICommand<UpdateGuestResult>;
    public record UpdateGuestResult(bool IsSuccess);

    public class UpdateGuestCommandValidator : AbstractValidator<UpdateGuestCommand>
    {
        public UpdateGuestCommandValidator()
        {
            RuleFor(x => x.GuestId).NotEmpty().WithMessage("Guest ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.DateofBirst).NotEmpty().WithMessage("Date of birth is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            //RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required").Matches(@"^\d+$")
            //    .WithMessage("Phone number must be numeric");
            //RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
        }
    }
    public class UpdateGuestHandler(IGuestRepository repository)
        : ICommandHandler<UpdateGuestCommand, UpdateGuestResult>
    {
        public async Task<UpdateGuestResult> Handle(UpdateGuestCommand command, CancellationToken cancellationToken)
        {

            var result = await repository.GetGuestById(command.GuestId, cancellationToken);

            result.UserId = command.UserId;
            result.FirstName = command.FirstName;
            result.LastName = command.LastName;
            result.DateofBirst = command.DateofBirst;
            result.Address = command.Address;

            var response = await repository.UpdateGuest(result, cancellationToken);
            //return
            return new UpdateGuestResult(response);
        }
    }
}
