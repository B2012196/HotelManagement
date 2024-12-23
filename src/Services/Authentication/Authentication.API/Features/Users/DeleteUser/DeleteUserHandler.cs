﻿namespace Authentication.API.Features.Users.DeleteUser
{
    public record DeleteUserCommand(Guid UserId) : ICommand<DeleteUserResult>;
    public record DeleteUserResult(bool IsSuccess);
    public class DeleteUserHandler(ApplicationDbContext context)
        : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == command.UserId, cancellationToken);
            if (user is null)
            {
                throw new UserNotFoundException(""+command.UserId);
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteUserResult(true);
        }
    }
}
