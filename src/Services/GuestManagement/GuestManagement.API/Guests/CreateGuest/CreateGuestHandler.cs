﻿namespace GuestManagement.API.Guests.CreateGuest
{
    public record CreateGuestCommand
        (Guid UserId, string FirstName, string LastName, DateTime DateofBirst, string Address) 
        : ICommand<CreateGuestResult>;

    public record CreateGuestResult(Guid GuestId);

    public class CreateGuestCommandValidator : AbstractValidator<CreateGuestCommand>
    {
        public CreateGuestCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.DateofBirst)
                .Must(BeAValidDate).WithMessage("Date of birth must be a valid date.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date of birth cannot be in the future.");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        }
        // Hàm kiểm tra DateOnly có hợp lệ không
        private bool BeAValidDate(DateTime date)
        {
            return date != default;
        }
    }
    public class CreateGuestHandler(IGuestRepository repository)
        : ICommandHandler<CreateGuestCommand, CreateGuestResult>
    {
        public async Task<CreateGuestResult> Handle(CreateGuestCommand command, CancellationToken cancellationToken)
        {
            var guest = new Guest
            {
                GuestId = Guid.NewGuid(),
                UserId = command.UserId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                DateofBirst = command.DateofBirst,
                Address = command.Address,
            };

            var result = await repository.CreateGuest(guest, cancellationToken);

            //return
            return new CreateGuestResult(result);

        }
    }
}
