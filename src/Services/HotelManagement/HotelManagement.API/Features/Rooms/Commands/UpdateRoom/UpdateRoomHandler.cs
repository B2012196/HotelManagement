﻿namespace HotelManagement.API.Features.Rooms.Commands.UpdateRoom
{
    public record UpdateRoomCommand
        (Guid RoomId, string Number, Guid HotelId, Guid TypeId, Guid StatusId) : ICommand<UpdateRoomResult>;
    public record UpdateRoomResult(bool IsSuccess);

    public class UpdateRoomValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("RoomId is required.");

            RuleFor(x => x.Number).NotEmpty().WithMessage("Room number is required.")
                .MaximumLength(10).WithMessage("Room number cannot exceed 10 characters.");

            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("HotelId is required.");

            RuleFor(x => x.TypeId)
                .NotEmpty().WithMessage("TypeId is required.");

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("StatusId is required.");
        }
    }
    public class UpdateRoomHandler(IRoomRepository repository)
        : ICommandHandler<UpdateRoomCommand, UpdateRoomResult>
    {
        public async Task<UpdateRoomResult> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
        {
            //Kiem tra 
            var room = await repository.GetRoomById(command.RoomId, cancellationToken);

            room.Number = command.Number;
            room.HotelId = command.HotelId;
            room.TypeId = command.TypeId;
            room.StatusId = command.StatusId;

            await repository.UpdateRoom(room, cancellationToken);
            return new UpdateRoomResult(true);
        }
    }
}
