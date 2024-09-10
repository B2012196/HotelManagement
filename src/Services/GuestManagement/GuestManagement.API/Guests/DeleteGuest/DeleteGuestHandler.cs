namespace GuestManagement.API.Guests.DeleteGuest
{
    public record DeleteGuestCommand(Guid GuestId) : ICommand<DeleteGuestResult>;
    public record DeleteGuestResult(bool IsSuccess);
    public class DeleteGuestHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteGuestCommand, DeleteGuestResult>
    {
        public async Task<DeleteGuestResult> Handle(DeleteGuestCommand command, CancellationToken cancellationToken)
        {
            var guest = await context.Guests.SingleOrDefaultAsync(g => g.GuestId == command.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new GuestNotFoundException(command.GuestId);
            }

            context.Guests.Remove(guest);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteGuestResult(true);
        }
    }
}
