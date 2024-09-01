namespace HotelManagement.API.Hotels.CreateHotel
{
    public record CreateHotelCommand 
        (string Name, string Address, string Phone, string Email, int Stars, DateTime CheckinTime, DateTime CheckoutTime)
        : ICommand<CreateHotelResult>;
    public record CreateHotelResult(Guid Id);
    public class CreateHotelHandler 
        : ICommandHandler<CreateHotelCommand, CreateHotelResult>
    {
        public async Task<CreateHotelResult> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
        {
            //create Hotel entity
            var hotel = new Hotel
            {
                Name = command.Name,
                Address = command.Address,
                Phone = command.Phone,
                Email = command.Email,
                Stars = command.Stars,
                CheckinTime = command.CheckinTime,
                CheckoutTime = command.CheckoutTime
            };

            //save database

            //return result
            return new CreateHotelResult(Guid.NewGuid());
        }
    }
}
