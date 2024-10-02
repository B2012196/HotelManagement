namespace GuestManagement.API.Guests.DeleteGuest
{
    public record DeleteGuestCommand(Guid GuestId) : ICommand<DeleteGuestResult>;
    public record DeleteGuestResult(bool IsSuccess);
    public class DeleteGuestHandler(IGuestRepository repository)
        : ICommandHandler<DeleteGuestCommand, DeleteGuestResult>
    {
        public async Task<DeleteGuestResult> Handle(DeleteGuestCommand command, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteGuest(command.GuestId, cancellationToken);

            return new DeleteGuestResult(result);
        }
    }
}
